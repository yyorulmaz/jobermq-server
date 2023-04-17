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

        private DistributorTypeEnum distributorType;
        public DistributorTypeEnum DistributorType { get => distributorType; set => distributorType = value; }
        private PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType { get => permissionType; set => permissionType = value; }
        private bool isDurable;
        public bool IsDurable { get => isDurable; set => isDurable = value; }


        public abstract bool MessageAdd(MessageDbo message);
    }
}