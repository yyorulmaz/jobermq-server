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
    internal class DfMessageDbOpr : DfOprRepository<MessageDbo>, IMessageDbOpr
    {
        internal DfMessageDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemMessage(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextMessage(configuration);
        }

        internal DfMessageDbOpr(IMemRepository<Guid, MessageDbo> dbMem, ITextRepository<MessageDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
