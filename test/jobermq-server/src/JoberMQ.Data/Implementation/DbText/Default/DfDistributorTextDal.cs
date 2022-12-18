using JoberMQ.Data.Implementation.Repository.DbText.Default;
using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfDistributorTextDal : DfDbTextRepository<DistributorDbo>, IDistributorTextDal
    {
        public DfDistributorTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
