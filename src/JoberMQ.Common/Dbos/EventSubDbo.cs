using JoberMQ.Common.Enums;
using JoberMQ.Library.Database.Base;

namespace JoberMQ.Common.Dbos
{
    public class EventSubDbo : DboPropertyGuidBase, IDboBase
    {
        public string EventKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public virtual string ClientKey { get; set; }
        public virtual string ClientGroupKey { get; set; }
    }
}
