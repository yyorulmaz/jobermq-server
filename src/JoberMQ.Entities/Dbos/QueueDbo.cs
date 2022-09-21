using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Enums.Queue;

namespace JoberMQ.Entities.Dbos
{
    internal class QueueDbo : DboPropertyGuidBase, IDboBase
    {
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public bool IsDurable { get; set; }
    }
}
