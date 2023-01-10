using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using JoberMQ.Queue.Abstraction;
using System;

namespace JoberMQ.Queue.Implementation
{
    internal abstract class MessageQueueBase : IMessageQueue
    {
        public MessageQueueBase(
            string queueKey,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IMemRepository<string, IClient> masterClient,
            IMemRepository<Guid, MessageDbo> masterMessages,
            IOprRepositoryGuid<MessageDbo> messageDbOpr,
            ref bool isJoberActive)
        {
            this.queueKey = queueKey;
            this.matchType = matchType;
            this.sendType = sendType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.isSendRuning = false;

            clientChilds = MemChildFactory.CreateChildGeneral<string, IClient>(Library.Database.Enums.MemChildFactoryEnum.Default, masterClient, false, true, true);
            this.masterQueue = masterMessages;
            this.messageDbOpr = messageDbOpr;
            this.isJoberActive = isJoberActive;
        }

        readonly string queueKey;
        public string QueueKey => queueKey;


        readonly MatchTypeEnum matchType;
        public MatchTypeEnum MatchType => matchType;


        readonly SendTypeEnum sendType;
        public SendTypeEnum SendType => sendType;


        readonly PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType => permissionType;


        public bool isDurable;
        public bool IsDurable => isDurable;


        bool isSendRuning;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }


        IMemChildGeneralRepository<string, IClient> clientChilds;
        public IMemChildGeneralRepository<string, IClient> ClientChilds { get => clientChilds; set => clientChilds = value; }


        IMemRepository<Guid, MessageDbo> masterQueue;

        protected int endConsumerNumber = 0;

        protected readonly IOprRepositoryGuid<MessageDbo> messageDbOpr;


        protected bool isJoberActive;


        public abstract bool MessageAdd(MessageDbo message);
        protected abstract void Qperation();
    }
}
