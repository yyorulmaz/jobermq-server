using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Distributor;

namespace JoberMQ.Server.Implementation.Distributor
{
    internal abstract class DistributorBase : IDistributor
    {
        private readonly string distributorKey;
        private readonly DistributorTypeEnum distributorType;
        private readonly PermissionTypeEnum permissionType;
        private readonly bool isDurable;
        
        public DistributorBase(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable)
        {
            this.distributorKey = distributorKey;
            this.distributorType = distributorType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
        }

        public string DistributorKey => distributorKey;
        public DistributorTypeEnum DistributorType => distributorType;
        public PermissionTypeEnum PermissionType => permissionType;
        public bool IsDurable => isDurable;


        public abstract JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}