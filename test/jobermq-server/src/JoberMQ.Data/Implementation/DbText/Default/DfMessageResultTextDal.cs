using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.Data.Implementation.Repository.DbText.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfMessageResultTextDal : DfDbTextRepository<MessageResultDbo>, IMessageResultTextDal
    {
        public DfMessageResultTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
