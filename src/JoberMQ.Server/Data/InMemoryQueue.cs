using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using System;
using System.Collections.Concurrent;

namespace JoberMQNEW.Server.Data
{
    internal class InMemoryQueue
    {
        internal static ConcurrentDictionary<string, IQueue> QueuesDatas = new ConcurrentDictionary<string, IQueue>();
        internal static ConcurrentDictionary<Guid, MessageDbo> QueueDataBase = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
