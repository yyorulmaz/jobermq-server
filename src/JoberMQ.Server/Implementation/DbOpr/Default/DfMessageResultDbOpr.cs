using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfMessageResultDbOpr : DfDbOprRepository<MessageResultDbo>, IMessageResultDbOpr
    {
        internal DfMessageResultDbOpr(IConcurrentDictionaryRepository<Guid, MessageResultDbo> dbMem, IDbTextRepository<MessageResultDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
