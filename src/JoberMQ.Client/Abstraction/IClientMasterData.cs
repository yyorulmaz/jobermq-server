using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Client.Abstraction
{
    public interface IClientMasterData
    {
        #region Data
        ConcurrentDictionary<string, IClient> Database { get; }
        #endregion

        #region Count
        int Count { get; }
        #endregion

        #region CRUD
        IClient Get(string key);
        IClient Get(Func<IClient, bool> filter);
        List<IClient> GetAll(Func<IClient, bool> filter = null);
        bool Add(string key, IClient value);
        bool Update(string key, IClient value);
        IClient Remove(string key);
        #endregion

        #region Changed
        event Action<string, IClient> ChangedAdded;
        event Action<string, IClient> ChangedUpdated;
        event Action<string, IClient> ChangedRemoved;
        #endregion
    }
}
