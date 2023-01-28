using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;

namespace JoberMQ.Queue.Abstraction
{
    internal interface IMessageQueue
    {
        public string QueueKey { get; }
        public MatchTypeEnum MatchType { get; }
        public SendTypeEnum SendType { get; }
        public PermissionTypeEnum PermissionType { get; }
        public bool IsDurable { get; }

        public bool IsSendRuning { get; set; }
        public bool MessageAdd(MessageDbo message);

        public IMemChildToolsRepository<string, IClient> ClientChilds { get; set; }
    }
}
