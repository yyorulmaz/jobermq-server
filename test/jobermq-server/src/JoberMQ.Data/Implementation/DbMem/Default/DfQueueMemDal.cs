using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfQueueMemDal : DfDbMemRepository<Guid, QueueDbo>, IQueueMemDal
    {
        public DfQueueMemDal(ConcurrentDictionary<Guid, QueueDbo> data) : base(data)
        {
        }
    }
}
