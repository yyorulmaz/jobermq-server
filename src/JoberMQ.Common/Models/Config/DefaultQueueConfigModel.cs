using JoberMQ.Common.Enums;

namespace JoberMQ.Common.Models.Config
{
    public class DefaultQueueConfigModel
    {
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
