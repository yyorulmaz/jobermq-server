using JoberMQ.Library.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Data
{
    internal class InMemoryMessage
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageMasterData = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
