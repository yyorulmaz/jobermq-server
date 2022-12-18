using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Message;
using JoberMQ.Entities.Enums.Routing;
using System;

namespace JoberMQ.Entities.Dbos
{
    internal class MessageResultDbo : DboPropertyGuidBase, IDboBase
    {
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

        #region 3.5 - OPTION
        public string GeneralData { get; set; }
        #endregion

        #endregion

        #region 7 - STATUS
        public bool IsSuccess { get; set; }
        #endregion

        #region 8 - CLONE CREATED
        public Guid? CreatedJobId { get; set; }
        public Guid? CreatedJobDetailId { get; set; }
        public Guid? CreatedJobTransactionId { get; set; }
        public Guid? CreatedJobTransactionDetailId { get; set; }
        #endregion
    }
}
