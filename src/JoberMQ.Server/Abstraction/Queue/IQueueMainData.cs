using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueueMainData
    {
        #region Data
        ConcurrentDictionary<Guid, MessageDbo> Data { get; }
        #endregion

        #region Count
        int Count { get; }
        #endregion

        #region CRUD
        MessageDbo Get(Guid key);
        MessageDbo Get(Func<MessageDbo, bool> filter);
        List<MessageDbo> GetAll(Func<MessageDbo, bool> filter = null);
        bool Add(Guid key, MessageDbo value);
        bool Update(Guid key, MessageDbo value);
        MessageDbo Remove(Guid key);
        #endregion

        #region Changed
        event Action<MessageDbo> ChangedAdded;
        event Action<MessageDbo> ChangedUpdated;
        event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
