using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Data
{
    internal class InMemoryMessage
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageMasterData = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
