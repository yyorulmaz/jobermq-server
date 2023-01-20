using JoberMQ.Common.Enums;
using JoberMQ.Common.Models;
using JoberMQ.Library.Database.Base;
using System;

namespace JoberMQ.Common.Dbos
{
    public class MessageResultDbo : DboPropertyGuidBase, IDboBase
    {
        public MessageTypeEnum MessageType { get; set; }
        public string Message { get; set; }
        public RoutingModel Routing { get; set; }
        public InfoModel Info { get; set; }
        public StatusModel Status { get; set; }

        public Guid? CreatedJobId { get; set; }
        public Guid? CreatedJobDetailId { get; set; }
        public Guid? CreatedJobTransactionId { get; set; }
        public Guid? CreatedJobTransactionDetailId { get; set; }
    }
}
