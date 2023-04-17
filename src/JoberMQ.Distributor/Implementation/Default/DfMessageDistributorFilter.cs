using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Queue.Abstraction;
using System;

namespace JoberMQ.Distributor.Implementation.Default
{
    internal class DfMessageDistributorFilter : MessageDistributorBase
    {
        public DfMessageDistributorFilter(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues) : base(distributorKey, distributorType, permissionType, isDurable, queues)
        {
        }

        public override bool MessageAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
