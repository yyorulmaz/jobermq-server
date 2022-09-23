using JoberMQ.Entities.Dbos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueueChildDataConcurrentQueue
    {
        #region Data
        IQueueMainData DataMain { get; }
        ConcurrentQueue<MessageDbo> Data { get; }
        #endregion

        #region Count
        int Count { get; }
        #endregion

        #region CRUD
        MessageDbo Get();
        public ConcurrentQueue<MessageDbo> GetAll();
        bool Add(MessageDbo value);
        MessageDbo Remove(Guid key);
        #endregion

        #region Changed
        event Action<MessageDbo> ChangedAdded;
        event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
