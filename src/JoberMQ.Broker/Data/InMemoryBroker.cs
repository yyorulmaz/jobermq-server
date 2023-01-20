using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Queue.Data
{
    internal class InMemoryBroker
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MasterMessages = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
