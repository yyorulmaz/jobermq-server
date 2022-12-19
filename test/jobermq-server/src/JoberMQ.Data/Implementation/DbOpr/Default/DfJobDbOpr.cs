using JoberMQ.Business.Abstraction.DbOpr;
using JoberMQ.Data.Abstraction.Repository.DbMem;
using JoberMQ.Data.Abstraction.Repository.DbText;
using JoberMQ.Data.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.Business.Implementation.DbOpr.Default
{
    internal class DfJobDbOpr : DfDbOprRepository<JobDbo>, IJobDbOpr
    {
        internal DfJobDbOpr(IDbMemRepository<Guid, JobDbo> dbMem, IDbTextRepository<JobDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
