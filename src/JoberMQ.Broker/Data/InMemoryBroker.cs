using JoberMQ.Common.Dbos;
using JoberMQ.Queue.Abstraction;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Queue.Data
{
    internal class InMemoryBroker
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MasterMessages = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
