using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using Albian.Pool.DbConnectionPool;
using log4net;

namespace Albian.Pool.Imp.DbConnectionPool
{
    /// <summary>
    ///  �����
    /// </summary>
    public class ObjectPool<T> : IObjectPool<T> where T : IDbConnection, new()
    {
        private readonly IPoolableObjectFactory<T> _factory;
        private IList<T> _busy = new List<T>();
        private bool _closed;
        private IList<T> _free = new List<T>();
        private static object locker = new object();
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ObjectPool(IPoolableObjectFactory<T> factory, int size)
        {
            if (null == factory)
            {
                if (null != Logger)
                    Logger.Error("���������ʱ�����쳣������ػ���������Ϊ��");
                throw new ArgumentNullException("factory", "���󴴽���������Ϊ�գ�");
            }
            _factory = factory;
            InitItems(size);

            if (null != Logger)
                Logger.InfoFormat("������Ѿ�����������س���Ϊ��{0}", size);
        }

        #region IObjectPool Members

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public T GetObject(string connectionString)
        {
            return DoGetObject(connectionString);
        }

        /// <summary>
        /// ��ʹ����ϵĶ��󷵻ص������.
        /// </summary>
        public void ReturnObject(T target)
        {
            DoReturnObject(target);
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

        protected T DoGetObject(string connectionString)
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
                if (!Monitor.TryEnter(locker,1000))//Ĭ�ϵ�1��
                {
                    if (null != Logger)
                        Logger.Warn("��������������޷�ȡ�ö��󣬶�������д���һ�������Ӷ���");
                    return RescueObject(connectionString);
                }
                isLock = true;
                while (_free.Count > 0)
                {
                    int i = _free.Count - 1;
                    T o = _free[i];
                    _free.RemoveAt(i);
                    _factory.ActivateObject(o, connectionString);
                    if (!_factory.ValidateObject(o)) continue;

                    _busy.Add(o);
                    if (null != Logger)
                        Logger.InfoFormat("���ӳ�״̬�����ڿ��ж��󳤶�Ϊ:{0},æµ���󳤶�Ϊ{1}.", NumIdle, NumActive);
                    return o;
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
            if (null != target)
            {
                _factory.DestroyObject(target);
                if (null != Logger) Logger.InfoFormat("�˶������ڸ����ӳ�");
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
            _free = new List<T>();
            _closed = true;
        }

        /// <summary>
        /// ǿ�д���һ������
        /// </summary>
        /// <returns></returns>
        public T RescueObject(string connectionString)
        {
            return DoRescueObject(connectionString);
        }

        protected T DoRescueObject(string connectionString)
        {
            T obj = _factory.CreateObject();
            _factory.ActivateObject(obj, connectionString);
            //obj.IsFromPool = false;
            return obj;
        }
    }
}