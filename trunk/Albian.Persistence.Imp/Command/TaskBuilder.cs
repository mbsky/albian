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

        public ITask BuildTaskForSingleObject<T>(T target)
            where T :IAlbianObject
        {
            ITask task = new Task();
            FakeCommandBuilder fakeBuilder = new FakeCommandBuilder();
            task.Context = fakeBuilder.GenerateSingleCreateStorage<T>(target);
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
                storageContext.ConnectionString = StorageParser.BuildConnectionString(storage);
                storageContext.Connection = DatabaseFactory.GetDbConnection(storage.DatabaseStyle, storageContext.ConnectionString);
               
            }
            return task;
        }
    }
}