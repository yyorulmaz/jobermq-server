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
    internal class DfQueueDbOpr : DfOprRepository<QueueDbo>, IQueueDbOpr
    {
        internal DfQueueDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemQueue(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextQueue(configuration);
        }

        internal DfQueueDbOpr(IMemRepository<Guid, QueueDbo> dbMem, ITextRepository<QueueDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
