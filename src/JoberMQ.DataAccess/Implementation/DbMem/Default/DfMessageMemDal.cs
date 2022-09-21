using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfMessageMemDal : ConcurrentDictionaryRepository<Guid, MessageDbo>, IMessageMemDal
    {
        public DfMessageMemDal()
        {
        }

        public DfMessageMemDal(ConcurrentDictionary<Guid, MessageDbo> data) : base(data)
        {
        }
    }
}
