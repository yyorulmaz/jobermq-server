using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Library.Database.Base;

namespace JoberMQ.Common.Dbos
{
    public class DistributorDbo : DboPropertyGuidBase, IDboBase
    {
        //[JsonProperty("1")]
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum  PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
