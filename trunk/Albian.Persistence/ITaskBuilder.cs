using System.Collections.Generic;
using Albian.ObjectModel;
using Albian.Persistence.Context;

namespace Albian.Persistence
{
    public interface ITaskBuilder
    {
        ITask BuildCreateTask<T>(IList<T> target)
            where T : IAlbianObject;

        ITask BuildCreateTask<T>(T target)
            where T : IAlbianObject;

        ITask BuildUpdateTask<T>(IList<T> target)
            where T : IAlbianObject;

        ITask BuildUpdateTask<T>(T target)
            where T : IAlbianObject;

        ITask BuildDeleteTask<T>(IList<T> target)
            where T : IAlbianObject;

        ITask BuildDeleteTask<T>(T target)
            where T : IAlbianObject;

        ITask BuildLoadTask<T>(IList<T> target)
            where T : IAlbianObject;

        ITask BuildLoadTask<T>(T target)
            where T : IAlbianObject;

        ITask BuildLoadTask<T>(string routingName, IList<T> target)
            where T : IAlbianObject;

        ITask BuildLoadTask<T>(string routingName, T target)
            where T : IAlbianObject;

        ITask BuildSaveTask<T>(IList<T> target)
            where T : IAlbianObject;

        ITask BuildSaveTask<T>(T target)
            where T : IAlbianObject;
    }
}