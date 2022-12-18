using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfUserMemDal : DfDbMemRepository<Guid, UserDbo>, IUserMemDal
    {
        public DfUserMemDal(ConcurrentDictionary<Guid, UserDbo> data) : base(data)
        {
        }
    }
}
