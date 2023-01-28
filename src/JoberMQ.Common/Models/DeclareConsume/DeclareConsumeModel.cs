using JoberMQ.Common.Enums.DeclareConsume;

namespace JoberMQ.Common.Models.DeclareConsume
{
    public class DeclareConsumeModel
    {
        public DeclareConsumeTypeEnum DeclareConsumeType { get; set; }
        public string DeclareKey { get; set; }
    }
}
