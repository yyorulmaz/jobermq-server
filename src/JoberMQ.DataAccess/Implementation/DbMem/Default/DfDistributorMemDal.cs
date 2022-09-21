using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfDistributorMemDal : ConcurrentDictionaryRepository<Guid, DistributorDbo>, IDistributorMemDal
    {
        public DfDistributorMemDal()
        {
        }

        public DfDistributorMemDal(ConcurrentDictionary<Guid, DistributorDbo> data) : base(data)
        {
        }
    }
}
