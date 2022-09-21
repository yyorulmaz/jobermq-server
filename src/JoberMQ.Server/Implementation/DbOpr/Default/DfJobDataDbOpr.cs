using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfJobDataDbOpr : DfDbOprRepository<JobDataDbo>, IJobDataDbOpr
    {
        internal DfJobDataDbOpr(IConcurrentDictionaryRepository<Guid, JobDataDbo> dbMem, IDbTextRepository<JobDataDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
