using JoberMQ.Library.Database.Base;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;

namespace JoberMQ.Common.Dbos
{
    internal class QueueDbo : DboPropertyGuidBase, IDboBase
    {
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
