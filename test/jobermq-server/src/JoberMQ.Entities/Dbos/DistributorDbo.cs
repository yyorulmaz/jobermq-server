using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using Newtonsoft.Json;

namespace JoberMQ.Entities.Dbos
{
    internal class DistributorDbo : DboPropertyGuidBase, IDboBase
    {
        //[JsonProperty("1")]
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
