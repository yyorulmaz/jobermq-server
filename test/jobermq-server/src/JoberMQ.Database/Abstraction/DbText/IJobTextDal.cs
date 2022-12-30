using JoberMQ.Database.Abstraction.Repository.DbText;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.Database.Abstraction.DbText
{
    internal interface IJobTextDal : IDbTextRepository<JobDbo>
    {
    }
}
