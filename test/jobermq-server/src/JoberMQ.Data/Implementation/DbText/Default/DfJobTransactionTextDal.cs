using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.Data.Implementation.Repository.DbText.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfJobTransactionTextDal : DfDbTextRepository<JobTransactionDbo>, IJobTransactionTextDal
    {
        public DfJobTransactionTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
