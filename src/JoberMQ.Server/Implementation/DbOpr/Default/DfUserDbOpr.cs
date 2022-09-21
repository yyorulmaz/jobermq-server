using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using System;
using System.Linq;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfUserDbOpr : DfDbOprRepository<UserDbo>, IUserDbOpr
    {
        internal DfUserDbOpr(IConcurrentDictionaryRepository<Guid, UserDbo> dbMem, IDbTextRepository<UserDbo> dbText) : base(dbMem, dbText)
        {
        }

        public bool Check(string userName, string password)
        {
            var check = DbMem.Data.Values.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            if (check != null)
                return true;
            else
                return false;
        }
    }
}
