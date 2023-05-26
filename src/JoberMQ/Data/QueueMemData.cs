using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class QueueMemData
    {
        internal static ConcurrentDictionary<Guid, QueueDbo> QueueDatas = new ConcurrentDictionary<Guid, QueueDbo>();
    }
}
