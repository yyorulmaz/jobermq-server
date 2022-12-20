using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Database.Abstraction.Repository.DbText;
using JoberMQ.Database.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using System;
using System.Linq;

namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfUserDbOpr : DfDbOprRepository<UserDbo>, IUserDbOpr
    {
        internal DfUserDbOpr(IDbMemRepository<Guid, UserDbo> dbMem, IDbTextRepository<UserDbo> dbText) : base(dbMem, dbText)
        {
        }

        public bool Check(string userName, string password)
        {
            var check = DbMem.MasterData.Values.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            if (check != null)
                return true;
            else
                return false;
        }
    }
}
