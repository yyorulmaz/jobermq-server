using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfJobTransactionMem : DfMemRepository<Guid, JobTransactionDbo>, IJobTransactionDbMem
    {
        public DfJobTransactionMem(ConcurrentDictionary<Guid, JobTransactionDbo> data) : base(data)
        {
        }
    }
}
