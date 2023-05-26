using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class MessageMemData
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageDatas = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
