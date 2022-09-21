using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfQueueMemDal : ConcurrentDictionaryRepository<Guid, QueueDbo>, IQueueMemDal
    {
        public DfQueueMemDal()
        {
        }

        public DfQueueMemDal(ConcurrentDictionary<Guid, QueueDbo> data) : base(data)
        {
        }
    }
}
