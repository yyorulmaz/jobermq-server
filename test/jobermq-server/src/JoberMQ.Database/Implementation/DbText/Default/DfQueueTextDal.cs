using JoberMQ.Database.Abstraction.DbText;
using JoberMQ.Database.Implementation.Repository.DbText.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.Database.Implementation.DbText.Default
{
    internal class DfQueueTextDal : DfDbTextRepository<QueueDbo>, IQueueTextDal
    {
        public DfQueueTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
