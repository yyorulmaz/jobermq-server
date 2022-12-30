using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Enums.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class DefaultQueueConfigModel
    {
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
