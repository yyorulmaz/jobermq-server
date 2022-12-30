using JoberMQ.Database.Abstraction.DbText;
using JoberMQ.Database.Implementation.Repository.DbText.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.Database.Implementation.DbText.Default
{
    internal class DfUserTextDal : DfDbTextRepository<UserDbo>, IUserTextDal
    {
        public DfUserTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
