using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Abstraction.Text;
using JoberMQ.Common.Database.Repository.Implementation.Opr.Default;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Database.Factories;
using JoberMQ.Common.Dbos;
using System;

namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfDistributorDbOpr : DfOprRepository<DistributorDbo>, IDistributorDbOpr
    {
        internal DfDistributorDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemDistributor(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextDistributor(configuration);
        }

        internal DfDistributorDbOpr(IMemRepository<Guid, DistributorDbo> dbMem, ITextRepository<DistributorDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
