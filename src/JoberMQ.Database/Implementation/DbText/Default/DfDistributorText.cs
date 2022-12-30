using JoberMQ.Common.Database.Models;
using JoberMQ.Common.Database.Repository.Implementation.Text.Default;
using JoberMQ.Database.Abstraction.DbText;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Database.Implementation.DbText.Default
{
    internal class DfDistributorText : DfTextRepository<DistributorDbo>, IDistributorDbText
    {
        public DfDistributorText(TextFileConfigModel textFileConfig) : base(textFileConfig)
        {
        }
    }
}
