using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Queue.Abstraction;
using System;

namespace JoberMQ.Queue.Implementation
{
    internal abstract class MessageQueueBase : IMessageQueue
    {
        private readonly IConfigurationQueue configurationQueue;
        private readonly string queueKey;
        private readonly MatchTypeEnum matchType;
        private readonly SendTypeEnum sendType;
        private readonly PermissionTypeEnum permissionType;
        private readonly bool isDurable;
        private readonly IClientGroup clientGroup;
        protected IMemRepository<Guid, MessageDbo> queueDataBase;
        protected readonly IMessageDbOpr messageDbOpr;
        private bool isSendRuning;
        protected int endConsumerNumber = 0;
        public MessageQueueBase(
            IConfigurationQueue configurationQueue,
            string queueKey,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IClientGroup clientGroup,
            IMemRepository<Guid, MessageDbo> queueDataBase, 
            IMessageDbOpr messageDbOpr)
        {
            this.configurationQueue = configurationQueue;
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
