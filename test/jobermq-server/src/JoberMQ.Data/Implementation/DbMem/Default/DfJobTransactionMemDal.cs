using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfJobTransactionMemDal : DfDbMemRepository<Guid, JobTransactionDbo>, IJobTransactionMemDal
    {
        public DfJobTransactionMemDal(ConcurrentDictionary<Guid, JobTransactionDbo> data) : base(data)
        {
        }
    }
}
