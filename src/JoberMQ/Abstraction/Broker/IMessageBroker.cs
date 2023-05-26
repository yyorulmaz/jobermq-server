using System.Threading.Tasks;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Abstraction.Broker
{
    internal interface IMessageBroker
    {
        public Task<ResponseBaseModel> ImportDatabaseDistributors();
        public Task<ResponseBaseModel> ImportDatabaseQueues();
        public Task<ResponseBaseModel> CreateDefaultDistributors();
        public Task<ResponseBaseModel> CreateDefaultQueues();
        public Task<ResponseBaseModel> QueueSetMessages();


        public Task<ResponseBaseModel> DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseBaseModel> DistributorUpdate(string distributorKey, PermissionTypeEnum permissionType, bool isDurable);
        public Task<ResponseBaseModel> DistributorRemove(string distributorKey);


        public Task<ResponseBaseModel> QueueCreate(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isActive);
        public Task<ResponseBaseModel> QueueUpdate(string queueKey, string[] tags, PermissionTypeEnum permissionType, bool isDurable, bool isActive);
        public Task<ResponseBaseModel> QueueRemove(string queueKey);
        public Task<ResponseBaseModel> QueueMerge(string distributorKey, string queueKey);
    }
}
