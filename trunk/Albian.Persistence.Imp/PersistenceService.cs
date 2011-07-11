using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Albian.Kernel.Cached;
using Albian.Kernel.Service.Impl;
using Albian.Persistence.Context;
using Albian.Persistence.Imp.Command;
using Albian.Persistence.Imp.Parser.Impl;
using Albian.Persistence.Imp.TransactionCluster;
using Albian.Persistence.Model;
using log4net;
using Albian.Persistence.Imp.Query;

namespace Albian.Persistence.Imp
{
    public static class PersistenceService
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool Create<T>(T albianObject)
            where T : IAlbianObject
        {
            if (null == albianObject)
            {
                throw new ArgumentNullException("albianObject");
            }
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildCreateTask<T>(albianObject);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Create<T>(IList<T> albianObjects)
            where T : IAlbianObject
        {
            if (null == albianObjects)
            {
                throw new ArgumentNullException("albianObjects");
            }
            if (0 == albianObjects.Count)
            {
                throw new ArgumentException("albianObject count is 0.");
            }
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildCreateTask<T>(albianObjects);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Modify<T>(T albianObject)
            where T : IAlbianObject
        {
            if (null == albianObject)
            {
                throw new ArgumentNullException("albianObject");
            }

            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildModifyTask<T>(albianObject);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Modify<T>(IList<T> albianObjects)
            where T : IAlbianObject
        {
            if (null == albianObjects)
            {
                throw new ArgumentNullException("albianObjects");
            }
            if (0 == albianObjects.Count)
            {
                throw new ArgumentException("albianObject count is 0.");
            }
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildModifyTask<T>(albianObjects);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Remove<T>(T albianObject)
            where T : IAlbianObject
        {
            if (null == albianObject)
            {
                throw new ArgumentNullException("albianObject");
            }
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildRemoveTask<T>(albianObject);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Remove<T>(IList<T> albianObjects)
            where T : IAlbianObject
        {
            if (null == albianObjects)
            {
                throw new ArgumentNullException("albianObjects");
            }
            if (0 == albianObjects.Count)
            {
                throw new ArgumentException("albianObject count is 0.");
            }
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildRemoveTask<T>(albianObjects);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Save<T>(T albianObject) where T : IAlbianObject
        {
            if (null == albianObject)
            {
                throw new ArgumentNullException("albianObject");
            }

            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildSaveTask<T>(albianObject);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Save<T>(IList<T> albianObjects) where T : IAlbianObject
        {
            if (null == albianObjects)
            {
                throw new ArgumentNullException("albianObjects");
            }
            if (0 == albianObjects.Count)
            {
                throw new ArgumentException("albianObject count is 0.");
            }
            ITaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildSaveTask(albianObjects);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }


        public static T FindObject<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoFindObject<T>(routingName, where);

        }

        public static T FindObject<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoFindObject<T>(PersistenceParser.DefaultRoutingName,where); 
        }

        public static T FindObject<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            if (null == cmd)
            {
                throw new ArgumentNullException("cmd");
            }
            return DoFindObject<T>(cmd);
        }

        public static IList<T> FindObjects<T>(int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (0 == top)
            {
                throw new ArgumentException("The 'top' is 0.");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }

            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, top, where, orderby);
        }


        public static IList<T> FindObjects<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, null);
        }

        public static IList<T> FindObjects<T>(IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }

            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, orderby);
        }

        public static IList<T> FindObjects<T>(IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }

            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, 0, null, orderby);
        }

        public static IList<T> FindObjects<T>(string routingName, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }

            return DoFindObjects<T>(routingName, 0, where, orderby);
        }

        public static IList<T> FindObjects<T>(string routingName, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoFindObjects<T>(routingName, 0, null, orderby);; 
        }

        public static IList<T> FindObjects<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
           
            return DoFindObjects<T>(routingName, 0, where, null);;
        }

        public static IList<T> FindObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (0 == top)
            {
                throw new ArgumentException("The 'top' is 0.");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoFindObjects<T>(routingName,top,where,orderby);
        }

        public static IList<T> FindObjects<T>(string routingName, int top, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (0 == top)
            {
                throw new ArgumentException("The 'top' is 0.");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoFindObjects<T>(routingName, top, where, null);
        }

        public static IList<T> FindObjects<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            if (null == cmd)
            {
                throw new ArgumentNullException("cmd");
            }
            return DoFindObjects<T>(cmd);
        }

        public static T LoadObject<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoLoadObject<T>(routingName, where); ;

        }

        public static T LoadObject<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoLoadObject<T>(PersistenceParser.DefaultRoutingName, where);
        }

        public static T LoadObject<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            if (null == cmd)
            {
                throw new ArgumentNullException("cmd");
            }
            return DoLoadObject<T>(cmd);
        }

        public static IList<T> LoadObjects<T>(int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (0 == top)
            {
                throw new ArgumentException("The 'top' is 0.");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, top, where, orderby);
        }

        public static IList<T> LoadObjects<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, null);
        }

        public static IList<T> LoadObjects<T>(IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, orderby);
        }

        public static IList<T> LoadObjects<T>(IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }

            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, 0, null, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoLoadObjects<T>(routingName, 0, where, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoLoadObjects<T>(routingName, 0, null, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoLoadObjects<T>(routingName, 0, where, null);
        }

        public static IList<T> LoadObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (0 == top)
            {
                throw new ArgumentException("The 'top' is 0.");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            if (null == orderby)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == orderby.Length)
            {
                throw new ArgumentException("orderby length is 0.");
            }
            return DoLoadObjects<T>(routingName, top, where, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, int top, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (0 == top)
            {
                throw new ArgumentException("The 'top' is 0.");
            }
            if (null == where)
            {
                throw new ArgumentNullException("where");
            }
            if (0 == where.Length)
            {
                throw new ArgumentException("where length is 0.");
            }
            return DoLoadObjects<T>(routingName, top, where, null);
        }

        public static IList<T> LoadObjects<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            if (null == cmd)
            {
                throw new ArgumentNullException("cmd");
            }
            return DoLoadObjects<T>(cmd);
        }


        private static T DoFindObject<T>(string routingName, IFilterCondition[] where)
            where T : IAlbianObject, new()
        {
            try
            {
                string cachedKey = Utils.GetCacheKey<T>(routingName,0,where,null);
                IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("CachedService");
                if (cachedService.Exist(cachedKey))
                {
                    return (T)cachedService.Get(cachedKey);
                }

                T target = DoLoadObject<T>(routingName, where);
                cachedService.InsertOrUpdate(cachedKey,target);
                return target;
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("Find Object is error.info:{0}.", exc.Message);
                throw exc;
            }
        }

        private static T DoFindObject<T>(IDbCommand cmd)
           where T : IAlbianObject, new()
        {
            try
            {
                string cachedKey = Utils.GetCacheKey<T>(cmd);
                IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("CachedService");
                if (cachedService.Exist(cachedKey))
                {
                    return (T)cachedService.Get(cachedKey);
                }

                T target = DoLoadObject<T>(cmd);
                cachedService.InsertOrUpdate(cachedKey, target);
                return target;
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("Find Object is error..info:{0}.",exc.Message);
                throw exc;
            }
        }

        private static IList<T> DoFindObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
          where T : IAlbianObject, new()
        {
            try
            {
                string cachedKey = Utils.GetCacheKey<T>(routingName,top,where,orderby);
                IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("CachedService");
                if (cachedService.Exist(cachedKey))
                {
                    return (IList<T>) cachedService.Get(cachedKey);
                }
                IList<T> target = DoLoadObjects<T>(routingName,top, where,orderby);
                cachedService.InsertOrUpdate(cachedKey, target);
                return target;
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("Find Object is error..info:{0}.", exc.Message);
                throw exc;
            }
        }

        private static IList<T> DoFindObjects<T>(IDbCommand cmd)
          where T : IAlbianObject, new()
        {
            try
            {
                string cachedKey = Utils.GetCacheKey<T>(cmd);
                IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("CachedService");
                if (cachedService.Exist(cachedKey))
                {
                    return (IList<T>)cachedService.Get(cachedKey);
                }
                IList<T> target = DoLoadObjects<T>(cmd);
                cachedService.InsertOrUpdate(cachedKey, target);
                return target;
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("Find Object is error..info:{0}.", exc.Message);
                throw exc;
            }
        }

        private static T DoLoadObject<T>(string routingName, IFilterCondition[] where)
           where T : IAlbianObject, new()
        {
            try
            {
                ITaskBuilder taskBuilder = new TaskBuilder();
                ITask task = taskBuilder.BuildQueryTask<T>(routingName, 0, where, null);
                IQueryCluster query = new QueryCluster();
                return query.QueryObject<T>(task);
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("load Object is error..info:{0}.", exc.Message);
                throw exc;
            }
        }

        private static T DoLoadObject<T>(IDbCommand cmd)
           where T : IAlbianObject, new()
        {
            try
            {
                IQueryCluster query = new QueryCluster();
                return query.QueryObject<T>(cmd);
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("load Object is error..info:{0}.", exc.Message);
                throw exc;
            }
        }

        private static IList<T> DoLoadObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
          where T : IAlbianObject, new()
        {
            try
            {
                ITaskBuilder taskBuilder = new TaskBuilder();
                ITask task = taskBuilder.BuildQueryTask<T>(routingName, top, where, orderby);
                IQueryCluster query = new QueryCluster();
                return query.QueryObjects<T>(task);
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("Find Object is error..info:{0}.", exc.Message);
                throw exc;
            }
        }

        private static IList<T> DoLoadObjects<T>(IDbCommand cmd)
          where T : IAlbianObject, new()
        {
            try
            {
                IQueryCluster query = new QueryCluster();
                return query.QueryObjects<T>(cmd);
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.ErrorFormat("Find Object is error..info:{0}.", exc.Message);
                throw exc;
            }
        }

        #region no impl method

        //public static T FindObject<T>(string routingName, string[] propertyNames, IFilterCondition[] where)
        //    where T : IAlbianObject,new()
        //{
        //    return default(T);
        //}

        //public static T FindObject<T>(string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(string routingName, int top,string[] propertyNames, IFilterCondition[] where,IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(int top, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(int top, string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(int top, string[] propertyNames, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(string[] propertyNames, IOrderByCondition[] orderby)
        //    where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}
        //public static T FindObjects<T>(string routingName,string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T FindObjects<T>(string routingName, int top, string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObject<T>(string routingName, string[] propertyNames, IFilterCondition[] where)
        //   where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObject<T>(string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(string routingName, int top, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(int top, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(int top, string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(int top, string[] propertyNames, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(string[] propertyNames, IOrderByCondition[] orderby)
        //    where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}


        //public static T LoadObjects<T>(string routingName, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}

        //public static T LoadObjects<T>(string routingName, int top, string[] propertyNames, IFilterCondition[] where)
        //     where T : IAlbianObject, new()
        //{
        //    return default(T);
        //}


        #endregion
    }
}