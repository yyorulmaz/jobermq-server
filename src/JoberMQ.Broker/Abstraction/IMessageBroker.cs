using JoberMQ.Distributor.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Models.Base;
using JoberMQ.Library.Models.Response;
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


        public Task<ResponseModel> QueueCreate(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseModel> QueueUpdate(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseModel> QueueRemove(string queueKey);
        public Task<ResponseModel> QueueMerge(string distributorKey, string queueKey);


        public Task<ResponseModel> Brokering(MessageDbo message);
        public Task<ResponseModel> Brokering(List<MessageDbo> messages);


        public Task<ResponseModel> MessageAdd(MessageDbo message);
        public bool MessageAdd(List<MessageDbo> messages);
    }
}
