using JoberMQ.Library.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class MessageMemData
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageDatas = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
