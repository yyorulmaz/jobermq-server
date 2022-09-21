using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfJobDbOpr : DfDbOprRepository<JobDbo>, IJobDbOpr
    {
        internal DfJobDbOpr(IConcurrentDictionaryRepository<Guid, JobDbo> dbMem, IDbTextRepository<JobDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
