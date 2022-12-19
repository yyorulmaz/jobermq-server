using JoberMQ.Business.Abstraction.DbOpr;
using JoberMQ.Data.Abstraction.Repository.DbMem;
using JoberMQ.Data.Abstraction.Repository.DbText;
using JoberMQ.Data.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.Business.Implementation.DbOpr.Default
{
    internal class DfJobTransactionDbOpr : DfDbOprRepository<JobTransactionDbo>, IJobTransactionDbOpr
    {
        internal DfJobTransactionDbOpr(IDbMemRepository<Guid, JobTransactionDbo> dbMem, IDbTextRepository<JobTransactionDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
