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
    internal class DfJobDbOpr : DfDbOprRepository<JobDbo>, IJobDbOpr
    {
        internal DfJobDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemJob(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextJob(configuration);
        }

        internal DfJobDbOpr(IDbMemRepository<Guid, JobDbo> dbMem, IDbTextRepository<JobDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
