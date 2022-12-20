using JoberMQ.Database.Abstraction.Repository.DbOpr;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.Database.Abstraction.DbOpr
{
    internal interface IUserDbOpr : IDbOprRepository<UserDbo>
    {
        public bool Check(string userName, string password);
    }
}
