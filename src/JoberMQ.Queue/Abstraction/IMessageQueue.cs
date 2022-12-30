using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;

namespace JoberMQ.Queue.Abstraction
{
    internal interface IMessageQueue
    {
        public string QueueKey { get; }
        public MatchTypeEnum MatchType { get; }
        public SendTypeEnum SendType { get; }
        public PermissionTypeEnum PermissionType { get; }
        public bool IsDurable { get; }


        public IClientGroup ClientGroup { get; }
        


        public bool IsSendRuning { get; set; }
        public bool QueueAdd(MessageDbo message);
    }
}
