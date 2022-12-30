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
    internal class DfJobDbOpr : DfOprRepository<JobDbo>, IJobDbOpr
    {
        internal DfJobDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemJob(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextJob(configuration);
        }

        internal DfJobDbOpr(IMemRepository<Guid, JobDbo> dbMem, ITextRepository<JobDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
