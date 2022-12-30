using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Database.Abstraction.DbOpr
{
    internal interface IUserDbOpr : IOprRepository<UserDbo>
    {
        public bool Check(string userName, string password);
    }
}
