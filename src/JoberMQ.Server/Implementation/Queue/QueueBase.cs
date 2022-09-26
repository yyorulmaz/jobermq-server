using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Implementation.Queue
{
    internal abstract class QueueBase : IQueue
    {
        private readonly BrokerConfigModel brokerConfig;
        private readonly string queueKey;
        private readonly MatchTypeEnum matchType;
        private readonly SendTypeEnum sendType;
        private readonly PermissionTypeEnum permissionType;
        private readonly bool isDurable;
        private readonly IClientGroup clientGroup;
        protected IQueueDataBase queueDataBase;
        protected readonly IMessageDbOpr messageDbOpr;
        private bool isSendRuning;
        protected int endConsumerNumber = 0;
        public QueueBase(
            BrokerConfigModel brokerConfig,
            string queueKey,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IClientGroup clientGroup,
            IQueueDataBase queueDataBase, 
            IMessageDbOpr messageDbOpr)
        {
            this.brokerConfig = brokerConfig;
            this.queueKey = queueKey;
            this.matchType = matchType;
            this.sendType = sendType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.clientGroup = clientGroup;
            this.queueDataBase = queueDataBase;
            this.messageDbOpr = messageDbOpr;
        }

        public string QueueKey => queueKey;
        public MatchTypeEnum MatchType => matchType;
        public SendTypeEnum SendType => sendType;
        public PermissionTypeEnum PermissionType => permissionType;
        public bool IsDurable => isDurable;
        public IClientGroup ClientGroup => clientGroup;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }

        public abstract bool QueueAdd(MessageDbo message);
        protected abstract void Qperation();
    }
}
