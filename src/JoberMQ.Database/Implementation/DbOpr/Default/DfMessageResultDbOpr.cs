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
    internal class DfMessageResultDbOpr : DfOprRepository<MessageResultDbo>, IMessageResultDbOpr
    {
        internal DfMessageResultDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemMessageResult(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextMessageResult(configuration);
        }

        internal DfMessageResultDbOpr(IMemRepository<Guid, MessageResultDbo> dbMem, ITextRepository<MessageResultDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
