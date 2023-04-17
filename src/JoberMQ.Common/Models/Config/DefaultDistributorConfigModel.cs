using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;

namespace JoberMQ.Common.Models.Config
{
    public class DefaultDistributorConfigModel
    {
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
