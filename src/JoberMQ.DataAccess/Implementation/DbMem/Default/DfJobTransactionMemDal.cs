using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfJobTransactionMemDal : ConcurrentDictionaryRepository<Guid, JobTransactionDbo>, IJobTransactionMemDal
    {
        public DfJobTransactionMemDal()
        {
        }

        public DfJobTransactionMemDal(ConcurrentDictionary<Guid, JobTransactionDbo> data) : base(data)
        {
        }
    }
}
