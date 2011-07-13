#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using Albian.Persistence.ConnectionPool;
using log4net;

#endregion

namespace Albian.Persistence.Imp.ConnectionPool
{
    /// <summary>
    ///  �����
    /// </summary>
    public class ConnectionPool<T> : IConnectionPool where T : IDbConnection, new()
    {
        private static readonly object locker = new object();
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IList<IDbConnection> _busy = new List<IDbConnection>();
        private readonly IPoolableConnectionFactory<T> _factory;
        private readonly int _maxSize = 30;
        private readonly int _minSize = 15;
        private bool _closed;
        private int _currentSize;
        private IList<IDbConnection> _free = new List<IDbConnection>();

        public ConnectionPool(IPoolableConnectionFactory<T> factory, int minSize, int maxSize)
        {
            if (null == factory)
            {
                if (null != Logger)
                    Logger.Error("���������ʱ�����쳣������ػ���������Ϊ��");
                throw new ArgumentNullException("factory", "���󴴽���������Ϊ�գ�");
            }
            _factory = factory;
            _minSize = minSize;
            _maxSize = maxSize;
            InitItems(minSize);
            _currentSize = minSize;

            if (null != Logger)
                Logger.InfoFormat("������Ѿ�����������س�ʼ����Ϊ��{0}����󳤶�Ϊ{1}", _minSize, _maxSize);
        }

        #region IConnectionPool Members

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public IDbConnection GetObject(string connectionString)
        {
            return DoGetObject(connectionString);
        }

        /// <summary>
        /// ��ʹ����ϵĶ��󷵻ص������.
        /// </summary>
        public void ReturnObject(IDbConnection target)
        {
            DoReturnObject((T) target);
        }

        /// <summary>
        /// �رն���ز��ͷų������е���Դ
        /// </summary>
        public void Close()
        {
            DoClose();
        }

        /// <summary>
        /// �õ���ǰ�����������ʹ�õĶ�����. 
        /// </summary>
        public int NumActive
        {
            get { return _busy.Count; }
        }

        /// <summary>
        /// �õ���ǰ������п��õĶ�����
        /// </summary>
        public int NumIdle
        {
            get { return _free.Count; }
        }

        /// <summary>
        /// ǿ�д���һ������
        /// </summary>
        /// <returns></returns>
        public IDbConnection RescueObject(string connectionString)
        {
            return DoRescueObject(connectionString);
        }

        #endregion

        protected void InitItems(int initialInstances)
        {
            if (initialInstances <= 0)
            {
                if (null != Logger)
                    Logger.Error("ʵ�����������ʱ�����쳣������س��Ȳ���Ϊ��");
                throw new ArgumentException("����س��Ȳ���Ϊ�գ�", "initialInstances");
            }
            for (int i = 0; i < initialInstances; ++i)
            {
                _free.Add(_factory.CreateObject());
            }
        }

        protected IDbConnection DoGetObject(string connectionString)
        {
            bool isLock = false;
            try
            {
                if (_closed)
                {
                    if (null != Logger)
                        Logger.Warn("�Ӷ�����л�ȡ����ʱ�����쳣��������Ѿ��رգ��޷�ȡ�ö��󣬶�������д���һ�������Ӷ���");
                    return RescueObject(connectionString);
                }
                if (!Monitor.TryEnter(locker, 1000)) //Ĭ�ϵ�1��
                {
                    if (null != Logger)
                        Logger.Warn("��������������޷�ȡ�ö��󣬶�������д���һ�������Ӷ���");
                    if (_currentSize < _maxSize)
                    {
                        IDbConnection target = RescueObject(connectionString);
                        _currentSize++;
                        return target;
                    }
                    else
                    {
                        Logger.Warn("��������������޷�ȡ�ö��󣬲��ҳ���size�Ѿ��ﵽ������ƣ��޷����д���һ�������Ӷ���");
                        throw new Exception("The poolsize is overflow.");
                    }
                }
                isLock = true;
                while (_free.Count > 0)
                {
                    int i = _free.Count - 1;
                    IDbConnection o = _free[i];
                    _free.RemoveAt(i);
                    _factory.ActivateObject((T) o, connectionString);
                    if (!_factory.ValidateObject((T) o)) continue;

                    _busy.Add(o);
                    if (null != Logger)
                        Logger.InfoFormat("���ӳ�״̬�����ڿ��ж��󳤶�Ϊ:{0},æµ���󳤶�Ϊ{1}.", NumIdle, NumActive);
                    return (T) o;
                }

                if (null != Logger)
                    Logger.InfoFormat("�Ӷ�����л�ȡ����ʱ�����쳣���������û�п��ö���!���ڿ��ж��󳤶�Ϊ:{0},æµ���󳤶�Ϊ{1}.", NumIdle, NumActive);
                return RescueObject(connectionString);
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("�Ӷ�����л�ȡ����ʱ�����쳣��{0}.", exc.Message);
                return RescueObject(connectionString);
            }
            finally
            {
                try
                {
                    if (isLock) Monitor.Exit(locker);
                }
                catch (Exception exc)
                {
                    if (isLock)
                    {
                        if (null != Logger)
                            Logger.ErrorFormat("��������ͷŶ�����ʱ�����쳣��{0}.", exc.Message);
                    }
                    else
                    {
                        if (null != Logger)
                            Logger.ErrorFormat("��δ��ȡ���Ķ������ͷ���ʱ�����쳣��{0}.", exc.Message);
                    }
                }
            }
        }

        protected bool DoReturnObject(T target)
        {
            if (_closed)
            {
                _factory.DestroyObject(target);
                if (null != Logger) Logger.Info("���ӳ��Ѿ��رգ��Żض����ͷţ�");
                return true;
            }
            lock (locker)
            {
                if (_busy.Contains(target))
                {
                    if (null != Logger) Logger.Info("���Ӷ���ʹ����ϣ�׼���Ż����ӳأ�");
                    _busy.Remove(target);
                    _factory.PassivateObject(target);
                    _free.Add(target);
                    if (null != Logger) Logger.Info("���Ӷ���ʹ����ϣ��Ż����ӳأ�");
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// �رն����
        /// </summary>
        private void DoClose()
        {
            _free = new List<IDbConnection>();
            _closed = true;
        }

        protected T DoRescueObject(string connectionString)
        {
            T obj = _factory.CreateObject();
            _factory.ActivateObject(obj, connectionString);
            return obj;
        }
    }
}