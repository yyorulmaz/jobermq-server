using JoberMQ.Business.Abstraction.DbOpr;
using JoberMQ.Data.Abstraction.Repository.DbMem;
using JoberMQ.Data.Abstraction.Repository.DbText;
using JoberMQ.Data.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.Business.Implementation.DbOpr.Default
{
    internal class DfDistributorDbOpr : DfDbOprRepository<DistributorDbo>, IDistributorDbOpr
    {
        internal DfDistributorDbOpr(IDbMemRepository<Guid, DistributorDbo> dbMem, IDbTextRepository<DistributorDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
