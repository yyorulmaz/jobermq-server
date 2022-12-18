using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.MemoryData
{
    internal class MessageMemData
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageDatas = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
