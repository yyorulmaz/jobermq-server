using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class DistributorMemData
    {
        internal static ConcurrentDictionary<Guid, DistributorDbo> DistributorDatas = new ConcurrentDictionary<Guid, DistributorDbo>();
    }
}
