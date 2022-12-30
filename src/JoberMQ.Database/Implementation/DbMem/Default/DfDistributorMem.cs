using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfDistributorMem : DfMemRepository<Guid, DistributorDbo>, IDistributorDbMem
    {
        public DfDistributorMem(ConcurrentDictionary<Guid, DistributorDbo> data) : base(data)
        {
        }
    }
}
