using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class DistributorMemData
    {
        internal static ConcurrentDictionary<Guid, DistributorDbo> DistributorDatas = new ConcurrentDictionary<Guid, DistributorDbo>();
    }
}
