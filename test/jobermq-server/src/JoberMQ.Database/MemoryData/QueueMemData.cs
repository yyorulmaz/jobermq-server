using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Database.MemoryData
{
    internal class QueueMemData
    {
        internal static ConcurrentDictionary<Guid, QueueDbo> QueueDatas = new ConcurrentDictionary<Guid, QueueDbo>();
    }
}
