using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Abstraction.Text;
using JoberMQ.Common.Database.Repository.Implementation.Opr.Default;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Common.Dbos;
using JoberMQ.Database.Factories;
using System;
using System.Linq;

namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfUserDbOpr : DfOprRepository<UserDbo>, IUserDbOpr
    {
        internal DfUserDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemUser(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextUser(configuration);
        }

        internal DfUserDbOpr(IMemRepository<Guid, UserDbo> dbMem, ITextRepository<UserDbo> dbText) : base(dbMem, dbText)
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
