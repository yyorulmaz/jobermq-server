using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Queue.Abstraction;

namespace JoberMQ.Distributor.Implementation
{
    internal abstract class MessageDistributorBase : IMessageDistributor
    {
        private readonly string distributorKey;
        private readonly DistributorTypeEnum distributorType;
        private readonly PermissionTypeEnum permissionType;
        private readonly bool isDurable;
        protected readonly IMemRepository<string, IMessageQueue> queues;


        public MessageDistributorBase(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues)
        {
            this.distributorKey = distributorKey;
            this.distributorType = distributorType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.queues = queues;
        }

        public string DistributorKey => distributorKey;
        public DistributorTypeEnum DistributorType => distributorType;
        public PermissionTypeEnum PermissionType => permissionType;
        public bool IsDurable => isDurable;


        public abstract bool MessageAdd(MessageDbo message);
    }
}