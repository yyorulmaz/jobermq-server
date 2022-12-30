using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfJobMemDal : DfDbMemRepository<Guid, JobDbo>, IJobMemDal
    {
        public DfJobMemDal(ConcurrentDictionary<Guid, JobDbo> data) : base(data)
        {
        }
    }
}
