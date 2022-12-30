using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class DefaultDistributorConfigModel
    {
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
