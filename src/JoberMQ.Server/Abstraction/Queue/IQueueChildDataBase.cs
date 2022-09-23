using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueueChildDataBase
    {
        #region Data
        public IQueueDataBase QueueDataBase { get; }
        #endregion

        #region Count
        public int Count { get; }
        #endregion

        #region CRUD
        public MessageDbo Get();
        public MessageDbo Get(Guid key);
        public bool Add(MessageDbo value);
        public MessageDbo Remove(Guid key);
        #endregion

        #region Changed
        public event Action<MessageDbo> ChangedAdded;
        public event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
