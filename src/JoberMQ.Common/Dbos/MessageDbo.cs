using JoberMQ.Common.Database.Base;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Status;
using System;

namespace JoberMQ.Common.Dbos
{
    internal class MessageDbo : DboPropertyGuidBase, IDboBase
    {
        #region 1 - PRODUCER
        public string ProducerClientKey { get; set; }
        public string ProducerClientGroupKey { get; set; }
        #endregion

        #region 3 - MESSAGE

        #region 3.1 - MESSAGE TYPE
        public MessageTypeEnum MessageType { get; set; }
        #endregion

        #region 3.2 - MESSAGE 
        public string Message { get; set; }
        #endregion

        #region 3.3 - ROUTING 
        public RoutingTypeEnum RoutingType { get; set; }
        public string DistributorKey { get; set; }
        public string RoutingKey { get; set; }
        public string ConsumerKey { get; set; }

        public string StartsWith { get; set; }
        public string Contains { get; set; }
        public string EndsWith { get; set; }
        #endregion

        #region 3.4 - RESULT ROUTING 
        public RoutingTypeEnum ResultRoutingType { get; set; }
        public bool IsResult { get; set; }
        public string ResultDistributorKey { get; set; }
        public string ResultRoutingKey { get; set; }
        public string ResultConsumerKey { get; set; }

        public string ResultStartsWith { get; set; }
        public string ResultContains { get; set; }
        public string ResultEndsWith { get; set; }
        #endregion

        #region 3.5 - OPTION
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeneralData { get; set; }
        public PriorityTypeEnum PriorityType { get; set; }
        #endregion

        #region 3.6 - CONSUMING
        public bool IsConsumingRetryPause { get; set; }
        public int ConsumingRetryMaxCount { get; set; }
        public int ConsumingRetryCounter { get; set; }
        public string ConsumingClientId { get; set; }
        public string ConsumingClientGroupKey { get; set; }
        #endregion

        #endregion

        #region 5 - TIMING
        public Guid? TriggerGroupsId { get; set; }
        #endregion

        #region 7 - STATUS
        public bool IsError { get; set; }
        public StatusTypeMessageEnum StatusTypeMessage { get; set; }
        public DateTime? TempAgainDate { get; set; }
        #endregion

        #region 8 - CLONE CREATED
        public Guid? CreatedJobId { get; set; }
        public Guid? CreatedJobDetailId { get; set; }
        public Guid? CreatedJobTransactionId { get; set; }
        public Guid? CreatedJobTransactionDetailId { get; set; }
        #endregion

        #region 9 - GROUP
        public Guid? EventGroupsId { get; set; }
        #endregion
    }
}
