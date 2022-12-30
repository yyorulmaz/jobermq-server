using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfDistributorMemDal : DfDbMemRepository<Guid, DistributorDbo>, IDistributorMemDal
    {
        public DfDistributorMemDal(ConcurrentDictionary<Guid, DistributorDbo> data) : base(data)
        {
        }
    }
}
