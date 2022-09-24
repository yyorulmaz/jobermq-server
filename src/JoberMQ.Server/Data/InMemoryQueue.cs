using JoberMQ.Entities.Dbos;
using JoberMQNEW.Server.Abstraction.Client;
using System;
using System.Collections.Concurrent;

namespace JoberMQNEW.Server.Data
{
    internal class InMemoryQueue
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> QueueDatas = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
