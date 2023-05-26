using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Response;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Queue.Abstraction
{
    internal interface IMessageQueue
    {
        public string DistributorKey { get; set; }
        public string QueueKey { get; set; }
        public QueueMatchTypeEnum MatchType { get; set; }
        public QueueOrderOfSendingTypeEnum QueueOrderOfSendingType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
        public bool IsActive { get; set; }

        public bool IsSendRuning { get; set; }
        public Task<ResponseModel> Queueing(MessageDbo message);

        public IClientChildData ClientChildData { get; set; }
        public int ChildMessageCount { get; }
    }
}
