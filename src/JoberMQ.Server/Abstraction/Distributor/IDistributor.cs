using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Models.Response;

namespace JoberMQ.Server.Abstraction.Distributor
{
    internal interface IDistributor
    {
        public string Name { get; }
        public DistributorTypeEnum Type { get; }
        public JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}
