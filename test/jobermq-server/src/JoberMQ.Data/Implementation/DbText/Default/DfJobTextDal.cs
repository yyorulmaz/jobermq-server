using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.Data.Implementation.Repository.DbText.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfJobTextDal : DfDbTextRepository<JobDbo>, IJobTextDal
    {
        public DfJobTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
