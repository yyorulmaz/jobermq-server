using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Common.Dbos
{
    internal class ProducerBase
    {
        public string ProducerClientKey { get; set; }
        public string ProducerClientGroupKey { get; set; }
    }

    internal class OptionBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeneralData { get; set; }
        public PriorityTypeEnum PriorityType { get; set; } = PriorityTypeEnum.None;
    }

    internal class PublisherBase
    {
        public PublisherTypeEnum PublisherType { get; set; }
    }

    internal class TimingBase
    {
        public TimingTypeEnum TimingType { get; set; }
        public ScheduleTypeEnum ScheduleType { get; set; }
        public string CronTime { get; set; }


        public int? ExecuteCountMax { get; set; }
        public int CreatedCount { get; set; }
        public bool IsCountMax { get; set; }


        public bool IsTrigger { get; set; }
        public bool ErrorWorkflowStop { get; set; }
        public Guid? TriggerJobId { get; set; }
        public bool IsTriggerMain { get; set; }


        public Guid? TriggerGroupsId { get; set; }
    }

    internal class RoutingBase
    {
        public RoutingTypeEnum RoutingType { get; set; }
        public string DistributorKey { get; set; }
        public string RoutingKey { get; set; }
        public string ConsumerKey { get; set; }

        public string StartsWith { get; set; }
        public string Contains { get; set; }
        public string EndsWith { get; set; }
    }

    internal class RoutingResultBase
    {
        public RoutingTypeEnum RoutingType { get; set; }
        public bool IsResult { get; set; }
        public string ResultDistributorKey { get; set; }
        public string ResultRoutingKey { get; set; }
        public string ResultConsumerKey { get; set; }

        public string ResultStartsWith { get; set; }
        public string ResultContains { get; set; }
        public string ResultEndsWith { get; set; }
    }

    internal class StatusBase
    {
        public bool IsCompleted { get; set; }
        public bool IsError { get; set; }
    }

    internal class ConsumingBase
    {
        public bool IsConsumingRetryPause { get; set; }
        public int ConsumingRetryMaxCount { get; set; }
        public int ConsumingRetryCounter { get; set; }
    }

    internal class MessageBase
    {
        public MessageTypeEnum MessageType { get; set; }
        public string Message { get; set; }
    }
}
