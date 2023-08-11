using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Abstraction.Distributor
{
    internal interface IMessageDistributor
    {
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; }
        public DistributorSearchSourceTypeEnum DistributorSearchSourceType { get; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
        public bool IsDefault { get; }

        public Task<ResponseModel> Distributoring(MessageDbo message);
    }
}
