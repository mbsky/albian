namespace Albian.Kernel.Cached
{
    public interface IExpiredCached : ICached
    {
        void Insert(string key, object value, int seconds);
        void Update(string key, object value, int seconds);
        void InsertOrUpdate(string key, object value, int seconds);
    }
}