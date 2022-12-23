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
    internal class DfMessageDbOpr : DfDbOprRepository<MessageDbo>, IMessageDbOpr
    {
        internal DfMessageDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemMessage(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextMessage(configuration);
        }

        internal DfMessageDbOpr(IDbMemRepository<Guid, MessageDbo> dbMem, IDbTextRepository<MessageDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
