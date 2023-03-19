using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Base;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Queue.Abstraction;
using System.Collections.Generic;

namespace JoberMQ.Broker.Abstraction
{
    internal interface IMessageBroker
    {
        IMemRepository<string, IMessageDistributor> MessageDistributors { get; set; }
        IMemRepository<string, IMessageQueue> MessageQueues { get; set; }

        public ResponseBaseModel DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable);
        //public ResponseBaseModel DistributorUpdate(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable);
        public ResponseBaseModel DistributorUpdate(string distributorKey, bool isDurable);
        public ResponseBaseModel DistributorRemove(string distributorKey);


        public ResponseBaseModel QueueCreate(string distributorKey, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable);
        public ResponseBaseModel QueueUpdate(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable);
        public ResponseBaseModel QueueRemove(string queueKey);
        public ResponseBaseModel QueueBind(string distributorKey, string queueKey);




        public bool MessageAdd(MessageDbo message);
        public bool QueueAdd(List<MessageDbo> messages);

        // check queue
    }
}
