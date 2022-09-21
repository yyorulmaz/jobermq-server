using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfDistributorDbOpr : DfDbOprRepository<DistributorDbo>, IDistributorDbOpr
    {
        internal DfDistributorDbOpr(IConcurrentDictionaryRepository<Guid, DistributorDbo> dbMem, IDbTextRepository<DistributorDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
