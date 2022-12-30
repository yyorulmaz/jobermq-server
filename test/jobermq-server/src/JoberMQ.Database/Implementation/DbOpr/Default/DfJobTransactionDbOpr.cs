using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Database.Abstraction.Repository.DbText;
using JoberMQ.Database.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Factories.DbOpr;
using System;

namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfJobTransactionDbOpr : DfDbOprRepository<JobTransactionDbo>, IJobTransactionDbOpr
    {
        internal DfJobTransactionDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemJobTransaction(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextJobTransaction(configuration);
        }

        internal DfJobTransactionDbOpr(IDbMemRepository<Guid, JobTransactionDbo> dbMem, IDbTextRepository<JobTransactionDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
