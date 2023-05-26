using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class JobTransactionMemData
    {
        internal static ConcurrentDictionary<Guid, JobTransactionDbo> JobTransactionDatas = new ConcurrentDictionary<Guid, JobTransactionDbo>();
    }
}
