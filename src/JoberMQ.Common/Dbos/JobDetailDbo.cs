using JoberMQ.Common.Enums;
using JoberMQ.Common.Models;
using JoberMQ.Library.Database.Base;
using System;

namespace JoberMQ.Common.Dbos
{
    public class JobDetailDbo : DboPropertyGuidBase, IDboBase
    {
        public Guid? JobId { get; set; }

        public MessageModel Message { get; set; }
        public bool IsResult { get; set; }
        public MessageModel ResultMessage { get; set; }


        public ConsumingModel Consuming { get; set; }
    }
}
