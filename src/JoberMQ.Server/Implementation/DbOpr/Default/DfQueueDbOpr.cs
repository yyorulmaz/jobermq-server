using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfQueueDbOpr : DfDbOprRepository<QueueDbo>, IQueueDbOpr
    {
        internal DfQueueDbOpr(IConcurrentDictionaryRepository<Guid, QueueDbo> dbMem, IDbTextRepository<QueueDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
