using JoberMQ.Entities.Dbos;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IDbChildFIFO : IDbChild
    {
        #region Data
        ConcurrentQueue<MessageDbo> Data { get; }
        #endregion
    }
}
