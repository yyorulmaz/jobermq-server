using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Distributor;

namespace JoberMQ.Entities.Dbos
{
    internal class DistributorDbo : DboPropertyGuidBase, IDboBase
    {
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public bool IsDurable { get; set; }
    }
}
