using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfJobMemDal : DfDbMemRepository<Guid, JobDbo>, IJobMemDal
    {
        public DfJobMemDal(ConcurrentDictionary<Guid, JobDbo> data) : base(data)
        {
        }
    }
}
