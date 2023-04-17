using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Library.Database.Enums;
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
            string distriburorKey,
            string queueKey,
            MatchTypeEnum  matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IMemRepository<string, IClient> masterClient,
            IMemRepository<Guid, MessageDbo> masterMessages,
            IOprRepositoryGuid<MessageDbo> messageDbOpr,
            ref bool isJoberActive)
        {
            this.distriburorKey = distriburorKey;
            this.queueKey = queueKey;
            this.matchType = matchType;
            this.sendType = sendType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.isSendRuning = false;

            this.masterQueue = masterMessages;
            this.messageDbOpr = messageDbOpr;
            this.isJoberActive = isJoberActive;
        }

        readonly string distriburorKey;
        public string DistributorKey => distriburorKey;

        readonly string queueKey;
        public string QueueKey => queueKey;


        MatchTypeEnum matchType;
        public MatchTypeEnum MatchType { get => matchType; set => matchType = value; }


        SendTypeEnum sendType;
        public SendTypeEnum SendType { get => sendType; set => sendType = value; }


        PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType { get => permissionType; set => permissionType = value; }


        public bool isDurable;
        public bool IsDurable { get => isDurable; set => isDurable = value; }


        bool isSendRuning;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }


        //IMemChildGeneralRepository<string, IClient> clientChilds;
        //public IMemChildGeneralRepository<string, IClient> ClientChilds { get => clientChilds; set => clientChilds = value; }
        public abstract IMemChildToolsRepository<string, IClient> ClientChilds { get; set; }


        IMemRepository<Guid, MessageDbo> masterQueue;

        protected int endConsumerNumber = 0;

        protected readonly IOprRepositoryGuid<MessageDbo> messageDbOpr;


        protected bool isJoberActive;


        public abstract bool MessageAdd(MessageDbo message);
        protected abstract void Qperation();
    }
}
