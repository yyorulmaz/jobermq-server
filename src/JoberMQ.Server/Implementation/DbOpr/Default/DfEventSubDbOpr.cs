using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfEventSubDbOpr : DfDbOprRepository<EventSubDbo>, IEventSubDbOpr
    {
        internal DfEventSubDbOpr(IConcurrentDictionaryRepository<Guid, EventSubDbo> dbMem, IDbTextRepository<EventSubDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
