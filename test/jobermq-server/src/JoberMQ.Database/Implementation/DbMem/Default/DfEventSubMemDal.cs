using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfEventSubMemDal : DfDbMemRepository<Guid, EventSubDbo>, IEventSubMemDal
    {
        public DfEventSubMemDal(ConcurrentDictionary<Guid, EventSubDbo> data) : base(data)
        {
        }
    }
}
