using JoberMQ.Library.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class QueueMemData
    {
        internal static ConcurrentDictionary<Guid, QueueDbo> QueueDatas = new ConcurrentDictionary<Guid, QueueDbo>();
    }
}
