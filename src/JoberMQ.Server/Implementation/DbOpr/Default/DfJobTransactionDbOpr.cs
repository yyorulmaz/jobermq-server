using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfJobTransactionDbOpr : DfDbOprRepository<JobTransactionDbo>, IJobTransactionDbOpr
    {
        internal DfJobTransactionDbOpr(IConcurrentDictionaryRepository<Guid, JobTransactionDbo> dbMem, IDbTextRepository<JobTransactionDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
