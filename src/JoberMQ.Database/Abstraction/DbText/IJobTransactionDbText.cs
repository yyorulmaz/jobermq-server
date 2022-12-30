using JoberMQ.Common.Database.Repository.Abstraction.Text;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Database.Abstraction.DbText
{
    internal interface IJobTransactionDbText : ITextRepository<JobTransactionDbo>
    {
    }
}
