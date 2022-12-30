using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Models.Response;

namespace JoberMQ.Server.Abstraction.Distributor
{
    internal interface IDistributor
    {
        public string DistributorKey { get; }
        public DistributorTypeEnum DistributorType { get; }
        public PermissionTypeEnum PermissionType { get; }
        public bool IsDurable { get; }

        public bool QueueAdd(MessageDbo message);
    }
}

