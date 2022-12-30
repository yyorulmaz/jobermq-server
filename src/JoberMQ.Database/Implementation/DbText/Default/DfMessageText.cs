using JoberMQ.Common.Database.Models;
using JoberMQ.Common.Database.Repository.Implementation.Text.Default;
using JoberMQ.Database.Abstraction.DbText;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Database.Implementation.DbText.Default
{
    internal class DfMessageText : DfTextRepository<MessageDbo>, IMessageDbText
    {
        public DfMessageText(TextFileConfigModel textFileConfig) : base(textFileConfig)
        {
        }
    }
}
