using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.DataAccess.Repository.DbText.Implementation;
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
