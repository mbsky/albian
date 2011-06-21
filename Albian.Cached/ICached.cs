namespace Albian.Cached
{
    public interface ICached
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