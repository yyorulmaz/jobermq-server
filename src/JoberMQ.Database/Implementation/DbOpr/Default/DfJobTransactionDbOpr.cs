using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Abstraction.Text;
using JoberMQ.Common.Database.Repository.Implementation.Opr.Default;
using JoberMQ.Common.Dbos;
using JoberMQ.Database.Factories;
using System;

namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfJobTransactionDbOpr : DfOprRepository<JobTransactionDbo>, IJobTransactionDbOpr
    {
        internal DfJobTransactionDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemJobTransaction(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextJobTransaction(configuration);
        }

        internal DfJobTransactionDbOpr(IMemRepository<Guid, JobTransactionDbo> dbMem, ITextRepository<JobTransactionDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
