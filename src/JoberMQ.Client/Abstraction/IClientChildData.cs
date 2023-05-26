using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Abstraction
{
    public interface IClientChildData
    {
        #region Data
        ConcurrentDictionary<string, IClient> DatabaseLocal { get; }
        IClientMasterData ClientMasterData { get; }
        #endregion

        #region Count
        int Count { get; }
        #endregion

        #region CRUD
        IClient Get(string key);
        IClient Get(Func<IClient, bool> filter);
        bool Update(string key, IClient value);
        IClient Remove(string key, IClient value);
        #endregion

        #region Changed
        event Action<string, IClient> ChangedAdded;
        event Action<string, IClient> ChangedUpdated;
        event Action<string, IClient> ChangedRemoved;
        #endregion
    }
}
