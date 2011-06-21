using System;

namespace Albian.Cached.Imp
{
    public class Cached : IExpiredCached
    {
        #region IExpiredCached 成员

        public void Insert(string key, object value, long seconds)
        {
            //HttpRuntime.Cache.Add(
        }

        public void Update(string key, object value, long seconds)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(string key, object value, long seconds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExpiredCached Members

        public bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Insert(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Update(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            //HttpRuntime.Cache.
        }

        #endregion
    }
}