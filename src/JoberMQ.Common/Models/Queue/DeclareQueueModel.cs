﻿using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Queue;

namespace JoberMQ.Common.Models.Queue
{
    public class DeclareQueueModel
    {
        public DeclareQueueOperationTypeEnum DeclareQueueOperationType { get; internal set; }
        public string DistributorKey { get; set; }
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
