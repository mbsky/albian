using Albian.Persistence.Context;

namespace Albian.Persistence.Imp.Command
{
    public interface ITaskBuilder
    {
        ITask BuildCreateTask<T>(T target)
            where T :IAlbianObject;
    }
}