using JoberMQ.Entities.Dbos;

namespace JoberMQ.Server.Abstraction.DbOpr
{
    internal interface IUserDbOpr : IDbOprRepository<UserDbo>
    {
        public bool Check(string userName, string password);
    }
}
