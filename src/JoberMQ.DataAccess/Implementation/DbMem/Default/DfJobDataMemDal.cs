using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfJobDataMemDal : ConcurrentDictionaryRepository<Guid, JobDataDbo>, IJobDataMemDal
    {
        public DfJobDataMemDal()
        {
        }

        public DfJobDataMemDal(ConcurrentDictionary<Guid, JobDataDbo> data) : base(data)
        {
        }
    }
}
