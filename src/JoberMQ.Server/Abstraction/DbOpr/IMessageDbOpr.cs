using JoberMQ.Entities.Dbos;

namespace JoberMQ.Server.Abstraction.DbOpr
{
    internal interface IMessageDbOpr : IDbOprRepository<MessageDbo>
    {
    }
}
