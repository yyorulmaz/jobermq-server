using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfJobMemDal : ConcurrentDictionaryRepository<Guid, JobDbo>, IJobMemDal
    {
        public DfJobMemDal()
        {
        }

        public DfJobMemDal(ConcurrentDictionary<Guid, JobDbo> data) : base(data)
        {
        }
    }
}
