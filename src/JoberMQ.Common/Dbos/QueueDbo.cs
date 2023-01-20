using JoberMQ.Common.Enums;
using JoberMQ.Library.Database.Base;

namespace JoberMQ.Common.Dbos
{
    public class QueueDbo : DboPropertyGuidBase, IDboBase
    {
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
