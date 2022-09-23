using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Implementation.Queue
{
    internal abstract class QueueBase : IQueue
    {
        private readonly string distributorName;
        private readonly string queueName;
        private readonly MatchTypeEnum matchType;
        private readonly SendTypeEnum sendType;
        private readonly IClientGroup clientGroup;
        protected IQueueDataBase queueDataBase;
        private bool isSendRuning;

        public QueueBase(
            BrokerConfigModel brokerConfig,
            string distributorName,
            string queueName,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            IClientGroup clientGroup,
            IQueueDataBase queueDataBase)
        {
            this.distributorName = distributorName;
            this.queueName = queueName;
            this.matchType = matchType;
            this.sendType = sendType;
            this.clientGroup = clientGroup;
            this.queueDataBase = queueDataBase;
        }

        public string DistributorName => distributorName;
        public string QueueName => queueName;
        public MatchTypeEnum MatchType => matchType;
        public SendTypeEnum SendType => sendType;
        public IClientGroup ClientGroup => clientGroup;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }

        public abstract JobDataAddResponseModel QueueAdd(MessageDbo message);
        protected abstract void Qperation();
    }
}
