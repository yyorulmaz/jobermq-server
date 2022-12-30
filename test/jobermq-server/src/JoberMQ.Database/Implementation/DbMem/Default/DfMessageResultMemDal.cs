using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfMessageResultMemDal : DfDbMemRepository<Guid, MessageResultDbo>, IMessageResultMemDal
    {
        public DfMessageResultMemDal(ConcurrentDictionary<Guid, MessageResultDbo> data) : base(data)
        {
        }
    }
}
