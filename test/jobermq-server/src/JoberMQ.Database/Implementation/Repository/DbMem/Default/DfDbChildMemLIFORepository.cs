using JoberMQ.Database.Abstraction.Repository.DbMem;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.Repository.DbMem.Default
{
    internal class DfDbChildMemLIFORepository<TKey, TValue> : IDbChildMemLIFORepository<TKey, TValue>
    {
        #region Constructor
        public DfDbChildMemLIFORepository(IDbMemRepository<TKey, TValue> masterData)
        {
            this.masterData = masterData;
            childData = new ConcurrentStack<TValue>();
        }
        #endregion

        #region Data
        private readonly IDbMemRepository<TKey, TValue> masterData;
        public IDbMemRepository<TKey, TValue> MasterData => masterData;


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
