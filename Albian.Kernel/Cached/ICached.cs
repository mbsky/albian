using Albian.Kernel.Service;

namespace Albian.Kernel.Cached
{
    public interface ICached : IAlbianService
    {
        bool Exist(string key);
        object Get(string key);
        void Insert(string key, object value);
        void Update(string key, object value);
        void InsertOrUpdate(string key, object value);
        void Remove(string key);
        void Remove();
    }
}