using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.MemoryData
{
    internal class DistributorMemData
    {
        internal static ConcurrentDictionary<Guid, DistributorDbo> DistributorDatas = new ConcurrentDictionary<Guid, DistributorDbo>();
    }
}
