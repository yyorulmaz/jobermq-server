using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfEventSubMemDal : DfDbMemRepository<Guid, EventSubDbo>, IEventSubMemDal
    {
        public DfEventSubMemDal(ConcurrentDictionary<Guid, EventSubDbo> data) : base(data)
        {
        }
    }
}
