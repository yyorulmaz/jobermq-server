using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfQueueMemDal : DfDbMemRepository<Guid, QueueDbo>, IQueueMemDal
    {
        public DfQueueMemDal(ConcurrentDictionary<Guid, QueueDbo> data) : base(data)
        {
        }
    }
}
