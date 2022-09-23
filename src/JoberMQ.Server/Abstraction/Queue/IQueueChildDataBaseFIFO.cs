using JoberMQ.Entities.Dbos;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueueChildDataBaseFIFO : IQueueChildDataBase
    {
        #region Data
        ConcurrentQueue<MessageDbo> Data { get; }
        #endregion
    }
}
