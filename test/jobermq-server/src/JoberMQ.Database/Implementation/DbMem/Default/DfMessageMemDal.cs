using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfMessageMemDal : DfDbMemRepository<Guid, MessageDbo>, IMessageMemDal
    {
        public DfMessageMemDal(ConcurrentDictionary<Guid, MessageDbo> data) : base(data)
        {
        }
    }
}
