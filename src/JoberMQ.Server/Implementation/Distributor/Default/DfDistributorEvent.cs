using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Models.Response;
using System;

namespace JoberMQ.Server.Implementation.Distributor.Default
{
    internal class DfDistributorEvent : DistributorBase
    {
        public DfDistributorEvent(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable) : base(distributorKey, distributorType, permissionType, isDurable)
        {
        }

        public override JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
