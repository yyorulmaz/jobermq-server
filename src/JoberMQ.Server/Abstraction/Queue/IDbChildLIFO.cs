using JoberMQ.Entities.Dbos;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IDbChildLIFO : IDbChild
    {
        #region Data
        ConcurrentStack<MessageDbo> Data { get; }
        #endregion
    }
}
