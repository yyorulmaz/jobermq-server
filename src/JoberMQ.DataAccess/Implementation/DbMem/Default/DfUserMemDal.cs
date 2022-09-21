using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfUserMemDal : ConcurrentDictionaryRepository<Guid, UserDbo>, IUserMemDal
    {
        public DfUserMemDal()
        {
        }

        public DfUserMemDal(ConcurrentDictionary<Guid, UserDbo> data) : base(data)
        {
        }
    }
}
