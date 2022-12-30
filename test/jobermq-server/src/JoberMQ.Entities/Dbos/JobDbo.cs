using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Publisher;
using JoberMQ.Entities.Enums.Routing;
using JoberMQ.Entities.Enums.Timing;
using System;
using System.Collections.Generic;

namespace JoberMQ.Entities.Dbos
{
    internal class JobDbo : DboPropertyGuidBase, IDboBase
    {
        #region 1 - PRODUCER
        public string ProducerClientKey { get; set; }
        public string ProducerClientGroupKey { get; set; }
        #endregion

        #region 2 - OPTION
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeneralData { get; set; }
        #endregion

        #region 4 - PUBLISHER
        public PublisherTypeEnum PublisherType { get; set; }
        #endregion

        #region 5 - TIMING
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
        #endregion

        #region 6 - RESULT ROUTING 
        public RoutingTypeEnum RoutingType { get; set; }
        public bool IsResult { get; set; }
        public string ResultDistributorKey { get; set; }
        public string ResultRoutingKey { get; set; }
        public string ResultConsumerKey { get; set; }

        public string ResultStartsWith { get; set; }
        public string ResultContains { get; set; }
        public string ResultEndsWith { get; set; }
        #endregion

        #region 7 - STATUS
        public bool IsCompleted { get; set; }
        public bool IsError { get; set; }
        #endregion

        #region 8 - CLONE CREATED
        public int Version { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        public ICollection<JobDetailDbo> Details { get; set; }
        #endregion
    }
}
