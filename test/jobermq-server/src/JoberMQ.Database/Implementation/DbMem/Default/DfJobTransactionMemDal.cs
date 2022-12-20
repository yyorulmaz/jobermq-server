using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfJobTransactionMemDal : DfDbMemRepository<Guid, JobTransactionDbo>, IJobTransactionMemDal
    {
        public DfJobTransactionMemDal(ConcurrentDictionary<Guid, JobTransactionDbo> data) : base(data)
        {
        }
    }
}
