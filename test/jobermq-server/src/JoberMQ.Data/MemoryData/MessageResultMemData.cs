using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.MemoryData
{
    internal class MessageResultMemData
    {
        internal static ConcurrentDictionary<Guid, MessageResultDbo> MessageResultDatas = new ConcurrentDictionary<Guid, MessageResultDbo>();
    }
}
