using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IDbChildPriority : IDbChild
    {
        #region Data
        ConcurrentDictionary<Guid, MessageDbo> Data { get; }
        #endregion
    }
}
