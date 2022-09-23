using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.Queue;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class DfQueueChildDataBasePriority : IQueueChildDataBasePriority
    {
        #region Constructor
        public DfQueueChildDataBasePriority(IQueueDataBase queueDataBase)
        {
            this.queueDataBase = queueDataBase;
            this.data = new ConcurrentDictionary<Guid, MessageDbo>();
        }
        #endregion

        #region Data
        private readonly IQueueDataBase queueDataBase;
        public IQueueDataBase QueueDataBase => queueDataBase;


        private readonly ConcurrentDictionary<Guid, MessageDbo> data;
        public ConcurrentDictionary<Guid, MessageDbo> Data => data;
        #endregion

        #region Count
        public int Count => data.Count;
        #endregion

        #region CRUD
        public MessageDbo Get()
        {
            throw new NotImplementedException();
        }
        public MessageDbo Get(Guid key)
        {
            data.TryGetValue(key, out MessageDbo value);
            return value;
        }
        public bool Add(MessageDbo value)
        {
            queueDataBase.Add(value.Id, value);
            var result = data.TryAdd(value.Id, value);
            if (result)
                ChangedAdded?.Invoke(value);
            return result;
        }
        public MessageDbo Remove(Guid key)
        {
            queueDataBase.Remove(key);
            data.TryRemove(key, out MessageDbo value);
            if (value != null)
                ChangedRemoved?.Invoke(value);
            return value;
        }
        #endregion

        #region Changed
        public event Action<MessageDbo> ChangedAdded;
        public event Action<MessageDbo> ChangedUpdated;
        public event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
