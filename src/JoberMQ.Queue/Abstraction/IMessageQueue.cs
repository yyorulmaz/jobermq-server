using JoberMQ.Client.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Models.Response;
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
