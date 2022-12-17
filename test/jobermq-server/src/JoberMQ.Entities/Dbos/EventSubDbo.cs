using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Queue;

namespace JoberMQ.Entities.Dbos
{
    internal class EventSubDbo : DboPropertyGuidBase, IDboBase
    {
        public string EventKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public virtual string ClientKey { get; set; }
        public virtual string ClientGroupKey { get; set; }
    }
}
