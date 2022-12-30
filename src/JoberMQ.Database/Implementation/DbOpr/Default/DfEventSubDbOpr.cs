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
    internal class DfEventSubDbOpr : DfOprRepository<EventSubDbo>, IEventSubDbOpr
    {
        internal DfEventSubDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemEventSub(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextEventSub(configuration);
        }

        internal DfEventSubDbOpr(IMemRepository<Guid, EventSubDbo> dbMem, ITextRepository<EventSubDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
