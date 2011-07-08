using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Albian.Persistence.Context;
using Albian.Persistence.Imp.Command;
using Albian.Persistence.Imp.TransactionCluster;
using Albian.Persistence.Model;

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
        


        public static T FindObject<T>(string routingName, string[] propertyNames, IFilterCondition[] where)
            where T : IAlbianObject,new()
        {
            return default(T);
        }

        public static T FindObject<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);

        }

        public static T FindObject<T>(string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObject<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObject<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return default(T);
        }


        public static T FindObjects<T>(string routingName, int top,string[] propertyNames, IFilterCondition[] where,IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(int top, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(int top,IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(int top, string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(int top, string[] propertyNames, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string[] propertyNames, IOrderByCondition[] orderby)
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName,string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName, int top, string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(string routingName, int top, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T FindObjects<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return default(T);
        }


        public static T LoadObject<T>(string routingName, string[] propertyNames, IFilterCondition[] where)
           where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObject<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);

        }

        public static T LoadObject<T>(string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObject<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObject<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, int top, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(int top, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(int top, string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(int top, string[] propertyNames, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string[] propertyNames, IOrderByCondition[] orderby)
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, string[] propertyNames, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, int top, string[] propertyNames, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(string routingName, int top, IFilterCondition[] where)
             where T : IAlbianObject, new()
        {
            return default(T);
        }

        public static T LoadObjects<T>(IDbCommand cmd)
            where T : IAlbianObject, new()
        {
            return default(T);
        }



    }
}