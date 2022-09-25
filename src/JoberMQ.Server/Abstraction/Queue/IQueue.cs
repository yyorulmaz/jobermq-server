using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueue
    {
        public string QueueKey { get; }
        public MatchTypeEnum MatchType { get; }
        public SendTypeEnum SendType { get; }
        public IClientGroup ClientGroup { get; }
        public bool IsSendRuning { get; set; }
        public JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}
