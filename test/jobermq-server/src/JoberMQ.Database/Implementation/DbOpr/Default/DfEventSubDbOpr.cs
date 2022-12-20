using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Database.Abstraction.Repository.DbText;
using JoberMQ.Database.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using System;
namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfEventSubDbOpr : DfDbOprRepository<EventSubDbo>, IEventSubDbOpr
    {
        internal DfEventSubDbOpr(IDbMemRepository<Guid, EventSubDbo> dbMem, IDbTextRepository<EventSubDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
