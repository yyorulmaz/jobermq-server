using JoberMQ.Data.Abstraction.Db;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.Implementation.Db.Default
{
    internal class DfDbChildLIFO<TKey, TValue> : IDbChildLIFO<TKey, TValue>
    {
        #region Constructor
        public DfDbChildLIFO(IDb<TKey, TValue> masterData)
        {
            this.masterData = masterData;
            childData = new ConcurrentStack<TValue>();
        }
        #endregion

        #region Data
        private readonly IDb<TKey, TValue> masterData;
        public IDb<TKey, TValue> MasterData => masterData;


        private readonly ConcurrentStack<TValue> childData;
        public ConcurrentStack<TValue> ChildData => childData;
        #endregion

        #region Count
        public int Count => childData.Count;
        #endregion

        #region CRUD
        public TValue Get()
        {
            childData.TryPeek(out var value);
            return value;
        }
        public TValue Get(TKey key)
        {
            throw new NotImplementedException();
        }
        public bool Add(TKey key, TValue value)
        {
            try
            {
                masterData.Add(key, value);
                childData.Push(value);
                ChangedAdded?.Invoke(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public TValue Remove(TKey key)
        {
            masterData.Remove(key);
            var result = childData.TryPeek(out var value);
            if (result)
                ChangedRemoved?.Invoke(value);
            return value;
        }
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
