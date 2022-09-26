using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Queue;
using System;

namespace JoberMQ.Server.Implementation.Distributor.Default
{
    internal class DfDistributorEvent : DistributorBase
    {
        public DfDistributorEvent(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IConcurrentDictionaryRepository<string, IQueue> queues) : base(distributorKey, distributorType, permissionType, isDurable, queues)
        {
        }

        public override bool QueueAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
