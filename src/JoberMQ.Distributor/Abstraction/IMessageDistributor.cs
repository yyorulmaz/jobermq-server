using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;

namespace JoberMQ.Distributor.Abstraction
{
    internal interface IMessageDistributor
    {
        public string DistributorKey { get; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }

        public bool MessageAdd(MessageDbo message); 
    }
}
