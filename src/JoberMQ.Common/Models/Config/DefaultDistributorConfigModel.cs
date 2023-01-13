using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Common.Models.Config
{
    public class DefaultDistributorConfigModel
    {
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
