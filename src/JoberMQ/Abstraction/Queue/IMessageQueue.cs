using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Abstraction.Queue
{
    internal interface IMessageQueue
    {
        public string QueueKey { get; set; }
        public string[] Tags { get; set; }
        public QueueMatchTypeEnum QueueMatchType { get; set; }
        public QueueOrderOfSendingTypeEnum QueueOrderOfSendingType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        public bool IsSendRuning { get; set; }
        public Task<ResponseModel> Queueing(MessageDbo message);

        public int ChildMessageCount { get; }
    }
}
