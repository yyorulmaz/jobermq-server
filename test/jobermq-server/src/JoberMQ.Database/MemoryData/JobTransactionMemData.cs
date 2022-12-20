using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Database.MemoryData
{
    internal class JobTransactionMemData
    {
        internal static ConcurrentDictionary<Guid, JobTransactionDbo> JobTransactionDatas = new ConcurrentDictionary<Guid, JobTransactionDbo>();
    }
}
