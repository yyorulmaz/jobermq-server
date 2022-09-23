using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueueChildDataConcurrentStack
    {
        #region Data
        IQueueMainData DataMain { get; }
        ConcurrentStack<MessageDbo> Data { get; }
        #endregion

        #region Count
        int Count { get; }
        #endregion

        #region CRUD
        MessageDbo Get();
        public ConcurrentStack<MessageDbo> GetAll();
        bool Add(MessageDbo value);
        MessageDbo Remove(Guid key);
        #endregion

        #region Changed
        event Action<MessageDbo> ChangedAdded;
        event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
