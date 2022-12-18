using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfMessageResultMemDal : DfDbMemRepository<Guid, MessageResultDbo>, IMessageResultMemDal
    {
        public DfMessageResultMemDal(ConcurrentDictionary<Guid, MessageResultDbo> data) : base(data)
        {
        }
    }
}
