using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfMessageDbOpr : DfDbOprRepository<MessageDbo>, IMessageDbOpr
    {
        internal DfMessageDbOpr(IConcurrentDictionaryRepository<Guid, MessageDbo> dbMem, IDbTextRepository<MessageDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
