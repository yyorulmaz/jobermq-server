using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.Queue;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Implementation.Queue.Default
{
    internal class DfDbChildLIFO : IDbChildLIFO
    {
        #region Constructor
        public DfDbChildLIFO(IDb queueDataBase)
        {
            this.queueDataBase = queueDataBase;
            data = new ConcurrentStack<MessageDbo>();
        }
        #endregion

        #region Data
        private readonly IDb queueDataBase;
        public IDb QueueDataBase => queueDataBase;


        private readonly ConcurrentStack<MessageDbo> data;
        public ConcurrentStack<MessageDbo> Data => data;
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
        public MessageDbo Get(Guid key)
        {
            throw new NotImplementedException();
        }
        public bool Add(MessageDbo value)
        {
            try
            {
                queueDataBase.Add(value.Id, value);
                data.Push(value);
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
            queueDataBase.Remove(key);
            var result = data.TryPeek(out var value);
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
