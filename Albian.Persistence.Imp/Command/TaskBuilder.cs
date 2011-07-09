using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Albian.Persistence.Context;
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
                IDictionary<string,IStorageContext> storageContexts = fakeBuilder.GenerateStorageContexts<T>(o, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildCreateFakeCommandByRouting);
                if (null == storageContexts || 0 == storageContexts.Count)
                {
                    if(null != Logger)
                        Logger.Error("The storagecontexts is empty.");
                    throw new Exception("The storagecontexts is null.");
                }
                if(null == task.Context || 0 == task.Context.Count)
                {
                    task.Context = storageContexts;
                    continue;
                }
                foreach (KeyValuePair<string, IStorageContext> storageContext in storageContexts)
                {
                    if (task.Context.ContainsKey(storageContext.Key))
                    {
                        task.Context[storageContext.Key].FakeCommand = task.Context.ContainsKey(storageContext.Key)
                                                               ? Utils.Concat(task.Context[storageContext.Key].FakeCommand,
                                                                        storageContext.Value.FakeCommand)
                                                               : storageContext.Value.FakeCommand;
                    }
                    else
                    {
                        task.Context.Add(storageContext);
                    }
                }
            }

            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }


        public ITask BuildModifyTask<T>(T target)
            where T : IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            task.Context = fakeBuilder.GenerateStorageContexts<T>(target, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildModifyFakeCommandByRouting);
            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }

        public ITask BuildModifyTask<T>(IList<T> target)
             where T : IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            foreach (T o in target)
            {
                IDictionary<string, IStorageContext> storageContexts = fakeBuilder.GenerateStorageContexts<T>(o, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildModifyFakeCommandByRouting);
                if (null == storageContexts || 0 == storageContexts.Count)
                {
                    if (null != Logger)
                        Logger.Error("The storagecontexts is empty.");
                    throw new Exception("The storagecontexts is null.");
                }
                if (null == task.Context || 0 == task.Context.Count)
                {
                    task.Context = storageContexts;
                    continue;
                }
                foreach (KeyValuePair<string, IStorageContext> storageContext in storageContexts)
                {
                    if (task.Context.ContainsKey(storageContext.Key))
                    {
                        task.Context[storageContext.Key].FakeCommand = task.Context.ContainsKey(storageContext.Key)
                                                               ? Utils.Concat(task.Context[storageContext.Key].FakeCommand,
                                                                        storageContext.Value.FakeCommand)
                                                               : storageContext.Value.FakeCommand;
                    }
                    else
                    {
                        task.Context.Add(storageContext);
                    }
                }
            }

            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }


        public ITask BuildRemoveTask<T>(T target)
            where T : IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            task.Context = fakeBuilder.GenerateStorageContexts<T>(target, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildDeleteFakeCommandByRouting);
            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }

        public ITask BuildRemoveTask<T>(IList<T> target)
             where T : IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            foreach (T o in target)
            {
                IDictionary<string, IStorageContext> storageContexts = fakeBuilder.GenerateStorageContexts<T>(o, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildDeleteFakeCommandByRouting);
                if (null == storageContexts || 0 == storageContexts.Count)
                {
                    if (null != Logger)
                        Logger.Error("The storagecontexts is empty.");
                    throw new Exception("The storagecontexts is null.");
                }
                if (null == task.Context || 0 == task.Context.Count)
                {
                    task.Context = storageContexts;
                    continue;
                }
                foreach (KeyValuePair<string, IStorageContext> storageContext in storageContexts)
                {
                    if (task.Context.ContainsKey(storageContext.Key))
                    {
                        task.Context[storageContext.Key].FakeCommand = task.Context.ContainsKey(storageContext.Key)
                                                               ? Utils.Concat(task.Context[storageContext.Key].FakeCommand,
                                                                        storageContext.Value.FakeCommand)
                                                               : storageContext.Value.FakeCommand;
                    }
                    else
                    {
                        task.Context.Add(storageContext);
                    }
                }
            }

            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }


        public ITask BuildSaveTask<T>(T target)
            where T : IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            task.Context = fakeBuilder.GenerateStorageContexts<T>(target, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildSaveFakeCommandByRouting);
            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }

        public ITask BuildSaveTask<T>(IList<T> target)
             where T : IAlbianObject
        {
            ITask task = new Task();
            IFakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            foreach (T o in target)
            {
                IDictionary<string, IStorageContext> storageContexts = fakeBuilder.GenerateStorageContexts<T>(o, fakeBuilder.GenerateFakeCommandByRoutings, fakeBuilder.BuildSaveFakeCommandByRouting);
                if (null == storageContexts || 0 == storageContexts.Count)
                {
                    if (null != Logger)
                        Logger.Error("The storagecontexts is empty.");
                    throw new Exception("The storagecontexts is null.");
                }
                if (null == task.Context || 0 == task.Context.Count)
                {
                    task.Context = storageContexts;
                    continue;
                }
                foreach (KeyValuePair<string, IStorageContext> storageContext in storageContexts)
                {
                    if (task.Context.ContainsKey(storageContext.Key))
                    {
                        task.Context[storageContext.Key].FakeCommand = task.Context.ContainsKey(storageContext.Key)
                                                               ? Utils.Concat(task.Context[storageContext.Key].FakeCommand,
                                                                        storageContext.Value.FakeCommand)
                                                               : storageContext.Value.FakeCommand;
                    }
                    else
                    {
                        task.Context.Add(storageContext);
                    }
                }
            }

            foreach (KeyValuePair<string, IStorageContext> context in task.Context)
            {
                IStorageContext storageContext = context.Value;
                object oStorage = StorageCache.Get(context.Key);
                if (null == oStorage)
                {
                    if (null != Logger)
                        Logger.ErrorFormat("There is no {0} storage attribute in the storage cached.", storageContext.StorageName);
                    return null;
                }
                IStorageAttribute storage = (IStorageAttribute)oStorage;
                storageContext.Storage = storage;
            }
            return task;
        }
    }
}