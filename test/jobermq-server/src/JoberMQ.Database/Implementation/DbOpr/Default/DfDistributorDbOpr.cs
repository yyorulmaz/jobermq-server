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
    internal class DfDistributorDbOpr : DfDbOprRepository<DistributorDbo>, IDistributorDbOpr
    {
        internal DfDistributorDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemDistributor(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextDistributor(configuration);
        }

        internal DfDistributorDbOpr(IDbMemRepository<Guid, DistributorDbo> dbMem, IDbTextRepository<DistributorDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
