using JoberMQ.Distributor.Abstraction;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Response;
using JoberMQ.Queue.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoberMQ.Broker.Abstraction
{
    internal interface IMessageBroker
    {
        event Action<bool> IsJoberActiveEvent;

        IMemRepository<string, IMessageDistributor> MessageDistributors { get; set; }
        IMemRepository<string, IMessageQueue> MessageQueues { get; set; }


        public Task<ResponseBaseModel> ImportDatabaseDistributors();
        public Task<ResponseBaseModel> ImportDatabaseQueues();
        public Task<ResponseBaseModel> CreateDefaultDistributors();
        public Task<ResponseBaseModel> CreateDefaultQueues();
        public Task<ResponseBaseModel> QueueSetMessages();


        public Task<ResponseModel> DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseModel> DistributorUpdate(string distributorKey, bool isDurable);
        public Task<ResponseModel> DistributorRemove(string distributorKey);


        public Task<ResponseModel> QueueCreate(string queueKey, QueueMatchTypeEnum matchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseModel> QueueUpdate(string queueKey, QueueMatchTypeEnum matchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseModel> QueueRemove(string queueKey);
        public Task<ResponseModel> QueueMerge(string distributorKey, string queueKey);


        public Task<ResponseModel> Brokering(MessageDbo message);
        public Task<ResponseModel> Brokering(List<MessageDbo> messages);


        public Task<ResponseModel> MessageAdd(MessageDbo message);
        public bool MessageAdd(List<MessageDbo> messages);
    }
}
