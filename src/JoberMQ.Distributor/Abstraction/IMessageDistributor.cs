using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Models.Response;
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Abstraction
{
    internal interface IMessageDistributor
    {
        public string DistributorKey { get; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }

        public Task<ResponseModel> Distributoring(MessageDbo message);
    }
}
