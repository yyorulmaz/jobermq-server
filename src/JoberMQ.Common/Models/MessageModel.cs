using JoberMQ.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Common.Models
{
    public class MessageModel
    {
        public MessageTypeEnum MessageType { get; set; }
        public string Message { get; set; }
        public RoutingModel Routing { get; set; }
        public InfoModel Info { get; set; }
        public string GeneralData { get; set; }
        public PriorityTypeEnum PriorityType { get; set; } = PriorityTypeEnum.None;
    }
}
