namespace Albian.Cached
{
    public interface IExpiredCached : ICached
    {
        void Insert(string key, object value, long seconds);
        void Update(string key, object value, long seconds);
        void InsertOrUpdate(string key, object value, long seconds);
    }
}