using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Queue.Abstraction;
using System;

namespace JoberMQ.Distributor.Implementation.Default
{
    internal class DfMessageDistributorEvent : MessageDistributorBase
    {
        public DfMessageDistributorEvent(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues) : base(distributorKey, distributorType, permissionType, isDurable, queues)
        {
        }

        public override bool QueueAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
