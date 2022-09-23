using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Distributor;

namespace JoberMQ.Server.Implementation.Distributor
{
    internal abstract class DfDistributorBase : IDistributor
    {
        private readonly string name;
        private readonly DistributorTypeEnum type;
        public DfDistributorBase(string name, DistributorTypeEnum type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name => name;
        public DistributorTypeEnum Type => type;

        public abstract JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}