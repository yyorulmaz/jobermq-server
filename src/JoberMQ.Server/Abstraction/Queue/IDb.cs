using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IDb
    {
        #region Data
        public ConcurrentDictionary<Guid, MessageDbo> Data { get; }
        #endregion

        #region Count
        public int Count { get; }
        #endregion

        #region CRUD
        public MessageDbo Get(Guid key);
        public MessageDbo Get(Func<MessageDbo, bool> filter);
        public List<MessageDbo> GetAll(Func<MessageDbo, bool> filter = null);
        public bool Add(Guid key, MessageDbo value);
        public bool Update(Guid key, MessageDbo value);
        public MessageDbo Remove(Guid key);
        #endregion

        #region Changed
        public event Action<MessageDbo> ChangedAdded;
        public event Action<MessageDbo> ChangedUpdated;
        public event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
