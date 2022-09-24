using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.Queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class QueueDataBase : IQueueDataBase
    {
        #region Constructor
        public QueueDataBase(ConcurrentDictionary<Guid, MessageDbo> data)
        {
            this.data = data;
        }
        #endregion

        #region Data
        private readonly ConcurrentDictionary<Guid, MessageDbo> data;
        public ConcurrentDictionary<Guid, MessageDbo> Data => data;
        #endregion

        #region Count
        public int Count => data.Count;
        #endregion

        #region CRUD
        public MessageDbo Get(Guid key)
        {
            data.TryGetValue(key, out MessageDbo value);
            return value;
        }
        public MessageDbo Get(Func<MessageDbo, bool> filter)
        {
            return data.Values.FirstOrDefault(filter);
        }
        public List<MessageDbo> GetAll(Func<MessageDbo, bool> filter = null)
        {
            if (filter == null)
                return data.Values.ToList();
            else
                return data.Values.Where(filter).ToList();
        }
        public bool Add(Guid key, MessageDbo value)
        {
            var result = data.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(value);
            return result;
        }
        public bool Update(Guid key, MessageDbo value)
        {
            var result = data.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(value);
            return result;
        }
        public MessageDbo Remove(Guid key)
        {
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
