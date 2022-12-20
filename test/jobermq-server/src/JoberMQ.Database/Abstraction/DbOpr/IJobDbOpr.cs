using JoberMQ.Database.Abstraction.Repository.DbOpr;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.Database.Abstraction.DbOpr
{
    internal interface IJobDbOpr : IDbOprRepository<JobDbo>
    {
    }
}
