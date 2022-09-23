using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.Queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class DfQueueChildDataConcurrentQueue : IQueueChildDataConcurrentQueue
    {
        #region Constructor
        public DfQueueChildDataConcurrentQueue(IQueueMainData dataMain)
        {
            this.dataMain = dataMain;
            this.data = new ConcurrentQueue<MessageDbo>();
        }
        #endregion

        #region Data
        private readonly IQueueMainData dataMain;
        public IQueueMainData DataMain => dataMain;


        private readonly ConcurrentQueue<MessageDbo> data;
        public ConcurrentQueue<MessageDbo> Data => data;
        #endregion

        #region Count
        public int Count => data.Count;
        #endregion

        #region CRUD
        public MessageDbo Get()
        {
            data.TryPeek(out var value);
            return value;
        }
        public ConcurrentQueue<MessageDbo> GetAll()
        {
            return data;
        }
        public bool Add(MessageDbo value)
        {
            try
            {
                dataMain.Add(value.Id, value);
                data.Enqueue(value);
                ChangedAdded?.Invoke(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public MessageDbo Remove(Guid key)
        {
            dataMain.Remove(key);
            var result = data.TryDequeue(out var value);
            if (result)
                ChangedRemoved?.Invoke(value);
            return value;
        }
        #endregion

        #region Changed
        public event Action<MessageDbo> ChangedAdded;
        public event Action<MessageDbo> ChangedRemoved;
        #endregion
    }
}
