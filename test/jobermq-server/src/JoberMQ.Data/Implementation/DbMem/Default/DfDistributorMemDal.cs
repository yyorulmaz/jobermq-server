using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfDistributorMemDal : DfDbMemRepository<Guid, DistributorDbo>, IDistributorMemDal
    {
        public DfDistributorMemDal(ConcurrentDictionary<Guid, DistributorDbo> data) : base(data)
        {
        }
    }
}
