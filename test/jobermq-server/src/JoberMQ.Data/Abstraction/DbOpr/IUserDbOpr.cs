using JoberMQ.Data.Abstraction.Repository.DbOpr;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.Business.Abstraction.DbOpr
{
    internal interface IUserDbOpr : IDbOprRepository<UserDbo>
    {
        public bool Check(string userName, string password);
    }
}
