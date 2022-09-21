using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfUserDbOpr : DfDbOprRepository<UserDbo>, IUserDbOpr
    {
        internal DfUserDbOpr(IConcurrentDictionaryRepository<Guid, UserDbo> dbMem, IDbTextRepository<UserDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
