using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;

namespace JoberMQ.Distributor.Abstraction
{
    internal interface IMessageDistributor
    {
        public string DistributorKey { get; }
        public DistributorTypeEnum DistributorType { get; }
        public PermissionTypeEnum PermissionType { get; }
        public bool IsDurable { get; }

        public bool MessageAdd(MessageDbo message); 
    }
}
