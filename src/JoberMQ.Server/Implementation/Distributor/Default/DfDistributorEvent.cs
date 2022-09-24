using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Models.Response;
using System;

namespace JoberMQ.Server.Implementation.Distributor.Default
{
    internal class DfDistributorEvent : DistributorBase
    {
        public DfDistributorEvent(string name, DistributorTypeEnum type) : base(name, type)
        {
        }

        public override JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
