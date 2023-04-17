using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;

namespace JoberMQ.Queue.Abstraction
{
    internal interface IMessageQueue
    {
        public string DistributorKey { get;  }
        public string QueueKey { get;  }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }

        public bool IsSendRuning { get; set; }
        public bool MessageAdd(MessageDbo message);

        public IMemChildToolsRepository<string, IClient> ClientChilds { get; set; }
    }
}
