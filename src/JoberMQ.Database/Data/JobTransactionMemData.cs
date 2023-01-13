using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class JobTransactionMemData
    {
        internal static ConcurrentDictionary<Guid, JobTransactionDbo> JobTransactionDatas = new ConcurrentDictionary<Guid, JobTransactionDbo>();
    }
}
