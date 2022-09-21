using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfEventSubMemDal : ConcurrentDictionaryRepository<Guid, EventSubDbo>, IEventSubMemDal
    {
        public DfEventSubMemDal()
        {
        }

        public DfEventSubMemDal(ConcurrentDictionary<Guid, EventSubDbo> data) : base(data)
        {
        }
    }
}
