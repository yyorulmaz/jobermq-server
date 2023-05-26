using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class InMemoryMessage
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageMasterData = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
