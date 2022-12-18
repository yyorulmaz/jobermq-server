using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfMessageMemDal : DfDbMemRepository<Guid, MessageDbo>, IMessageMemDal
    {
        public DfMessageMemDal(ConcurrentDictionary<Guid, MessageDbo> data) : base(data)
        {
        }
    }
}
