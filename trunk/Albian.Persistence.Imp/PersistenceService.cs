using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Albian.Persistence.Context;
using Albian.Persistence.Imp.Command;
using Albian.Persistence.Imp.TransactionCluster;
using Albian.Persistence.Model;
using Albian.Persistence.Imp.Parser;

namespace Albian.Persistence.Imp
{
    public static class PersistenceService
    {
        public static bool Create<T>(T entity)
            where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildCreateTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Create<T>(IList<T> entity)
            where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildCreateTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Modify<T>(T entity)
            where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildModifyTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Modify<T>(IList<T> entity)
            where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildModifyTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Remove<T>(T entity)
            where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildDeleteTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Remove<T>(IList<T> entity)
            where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildDeleteTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Save<T>(T entity) where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildSaveTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }

        public static bool Save<T>(IList<T> entity) where T : IAlbianObject
        {
            TaskBuilder builder = new TaskBuilder();
            ITask task = builder.BuildSaveTask<T>(entity);
            ITransactionClusterScope tran = new TransactionClusterScope();
            return tran.Execute(task);
        }


        public static T FindObject<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoFindObject<T>(routingName, where);

        }

        public static T FindObject<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoFindObject<T>(PersistenceParser.DefaultRoutingName,where); 
        }

        public static T FindObject<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return DoFindObject<T>(cmd);
        }

        public static IList<T> FindObjects<T>(int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, top, where, orderby);
        }


        public static IList<T> FindObjects<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, null);
        }

        public static IList<T> FindObjects<T>(IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, orderby);
        }

        public static IList<T> FindObjects<T>(IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(PersistenceParser.DefaultRoutingName, 0, null, orderby);
        }

        public static IList<T> FindObjects<T>(string routingName, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(routingName, 0, where, orderby);
        }

        public static IList<T> FindObjects<T>(string routingName, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(routingName, 0, null, orderby);; 
        }

        public static IList<T> FindObjects<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(routingName, 0, where, null);;
        }

        public static IList<T> FindObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(routingName,top,where,orderby);
        }

        public static IList<T> FindObjects<T>(string routingName, int top, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(routingName, top, where, null);
        }

        public static IList<T> FindObjects<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return DoFindObjects<T>(cmd);
        }

        public static T LoadObject<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoLoadObject<T>(routingName, where); ;

        }

        public static T LoadObject<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoLoadObject<T>(PersistenceParser.DefaultRoutingName, where);
        }

        public static T LoadObject<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return DoLoadObject<T>(cmd);
        }

        public static IList<T> LoadObjects<T>(int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, top, where, orderby);
        }

        public static IList<T> LoadObjects<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, null);
        }

        public static IList<T> LoadObjects<T>(IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, 0, where, orderby);
        }

        public static IList<T> LoadObjects<T>(IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(PersistenceParser.DefaultRoutingName, 0, null, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(routingName, 0, where, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(routingName, 0, null, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(routingName, 0, where, null);
        }

        public static IList<T> LoadObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(routingName, top, where, orderby);
        }

        public static IList<T> LoadObjects<T>(string routingName, int top, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(routingName, top, where, null);
        }

        public static IList<T> LoadObjects<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return DoLoadObjects<T>(cmd);
        }


        private static T DoFindObject<T>(string routingName, IFilterCondition[] where)
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        private static T DoFindObject<T>(IDbCommand cmd)
           where T : IAlbianObject, new()
        {
            return default(T);
        }

        private static IList<T> DoFindObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
          where T : IAlbianObject, new()
        {
            return new List<T>();
        }

        private static IList<T> DoFindObjects<T>(IDbCommand cmd)
          where T : IAlbianObject, new()
        {
            return new List<T>();
        }

        private static T DoLoadObject<T>(string routingName, IFilterCondition[] where)
           where T : IAlbianObject, new()
        {
            return default(T);
        }

        private static T DoLoadObject<T>(IDbCommand cmd)
           where T : IAlbianObject, new()
        {
            return default(T);
        }

        private static IList<T> DoLoadObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
          where T : IAlbianObject, new()
        {
            return new List<T>();
        }

        private static IList<T> DoLoadObjects<T>(IDbCommand cmd)
          where T : IAlbianObject, new()
        {
            return new List<T>();
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