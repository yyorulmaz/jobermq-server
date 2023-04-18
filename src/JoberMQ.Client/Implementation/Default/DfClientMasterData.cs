using JoberMQ.Client.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace JoberMQ.Client.Implementation.Default
{
    public class DfClientMasterData : IClientMasterData
    {
        #region Constructor
        public DfClientMasterData(ConcurrentDictionary<string, IClient> database)
        {
            this.database = database;
        }
        #endregion

        #region Data
        private readonly ConcurrentDictionary<string, IClient> database;
        public ConcurrentDictionary<string, IClient> Database => database;
        #endregion

        #region Count
        public int Count => database.Count;
        #endregion

        #region CRUD
        public IClient Get(string key)
        {
            database.TryGetValue(key, out IClient value);
            return value;
        }
        public IClient Get(Func<IClient, bool> filter)
        {
            return database.Values.FirstOrDefault(filter);
        }
        public List<IClient> GetAll(Func<IClient, bool> filter = null)
        {
            if (filter == null)
                return database.Values.ToList();
            else
                return database.Values.Where(filter).ToList();
        }
        public bool Add(string key, IClient value)
        {
            var result = database.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(key, value);
            return result;
        }
        public bool Update(string key, IClient value)
        {
            var result = database.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(key, value);
            return result;
        }
        public IClient Remove(string key)
        {
            database.TryRemove(key, out IClient value);
            if (value != null)
                ChangedRemoved?.Invoke(key, value);
            return value;
        }
        #endregion

        #region Changed
        public event Action<string, IClient> ChangedAdded;
        public event Action<string, IClient> ChangedUpdated;
        public event Action<string, IClient> ChangedRemoved;
        #endregion
    }
}
