using JoberMQ.Common.Database.Base;
using JoberMQ.Common.Enums.Queue;

namespace JoberMQ.Common.Dbos
{
    internal class EventSubDbo : DboPropertyGuidBase, IDboBase
    {
        public string EventKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public virtual string ClientKey { get; set; }
        public virtual string ClientGroupKey { get; set; }
    }
}
