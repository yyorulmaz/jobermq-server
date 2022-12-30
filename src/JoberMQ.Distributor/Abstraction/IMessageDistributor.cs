using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Abstraction
{
    internal interface IMessageDistributor
    {
        public string DistributorKey { get; }
        public DistributorTypeEnum DistributorType { get; }
        public PermissionTypeEnum PermissionType { get; }
        public bool IsDurable { get; }

        public bool QueueAdd(MessageDbo message);
    }
}
