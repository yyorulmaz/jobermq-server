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
    internal class DfMessageResultDbOpr : DfDbOprRepository<MessageResultDbo>, IMessageResultDbOpr
    {
        internal DfMessageResultDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemMessageResult(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextMessageResult(configuration);
        }

        internal DfMessageResultDbOpr(IDbMemRepository<Guid, MessageResultDbo> dbMem, IDbTextRepository<MessageResultDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
