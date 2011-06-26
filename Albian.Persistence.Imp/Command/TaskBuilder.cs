using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Albian.Persistence.Context;
using Albian.ObjectModel;
using Albian.Persistence.Imp.Cache;
using Albian.Persistence.Imp.Context;
using Albian.Persistence.Imp.Parser;
using log4net;
using Albian.Persistence.Model;
using System.Data.Common;

namespace Albian.Persistence.Imp.Command
{
    public class TaskBuilder : ITaskBuilder
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //protected ITask GenerateTask()
        //{
        //    //ITask task = new Task();
        //    IDictionary<string, IStorageContext> dic = new Dictionary<string, IStorageContext>();
        //    IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
        //    dic = fakeBuilder.GenerateStorageContexts<T>(target, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildCreateFakeCommandByRouting);
        //    foreach (KeyValuePair<string, IStorageContext> context in dic)
        //    {
        //        IStorageContext storageContext = context.Value;
        //        object oStorage = StorageCache.Get(context.Key);
        //        if (null == oStorage)
        //        {
        //            if (null != Logger)
        //                Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
        //            return null;
        //        }
        //        IStorageAttribute storage = (IStorageAttribute)oStorage;
        //        storageContext.Storage = storage;
        //    }
        //    //return task;
        //}

        public ITask BuildCreateTask<T>(T target)
            where T :IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            task.Context = fakeBuilder.GenerateStorageContexts<T>(target, fakeBuilder.GenerateFakeCommandByRoutings,fakeBuilder.BuildCreateFakeCommandByRouting);
            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if(null == oStorage)
                {
                    if(null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.",storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }

        public ITask BuildCreateTask<T>(IList<T> target)
             where T :IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            foreach (T o in target)
            {
                IDictionary<string,IStorageContext> dic = fakeBuilder.GenerateStorageContexts<T>(o, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildCreateFakeCommandByRouting);
                if(null == dic)
                {
                    if(null != Logger)
                        Logger.Error("The storagecontexts is empty.");
                    throw new Exception("The storagecontexts is null.");
                }
                if(null == task.Context)
                {
                    task.Context = dic;
                    continue;
                }
                //foreach(KeyValuePair<string,IStorageContext> c in dic)
                //{
                //    if(task.Context.ContainsKey(c.Key))
                //    {
                //        //IDictionary<string,DbParameter[]> fc = task.Context[c.Key].FakeCommand;
                //        if(null == fc)
                //        {
                //            task.Context[c.Key].FakeCommand = c.Value.FakeCommand;
                //            break;
                //        }
                //        //task.Context[c.Key].FakeCommand.Add
                //    }
                //    else
                //    {
                //    }
                //}

            }
            //task.Context = 
            //foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            //{
            //    IStorageContext storageContext = context.Value;
            //    object oStorage = StorageCache.Get(context.Key);
            //    if (null == oStorage)
            //    {
            //        if (null != Logger)
            //            Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
            //        return null;
            //    }
            //    IStorageAttribute storage = (IStorageAttribute)oStorage;
            //    storageContext.Storage = storage;
            //}
            return task;
        }
    }
}