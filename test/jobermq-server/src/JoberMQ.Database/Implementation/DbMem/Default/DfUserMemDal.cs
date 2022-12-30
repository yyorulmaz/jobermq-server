using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.Repository.DbMem.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfUserMemDal : DfDbMemRepository<Guid, UserDbo>, IUserMemDal
    {
        public DfUserMemDal(ConcurrentDictionary<Guid, UserDbo> data) : base(data)
        {
        }
    }
}
