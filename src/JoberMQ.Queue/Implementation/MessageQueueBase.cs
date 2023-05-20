using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Factories;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Models.Response;
using JoberMQ.Queue.Abstraction;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Queue.Implementation
{
    internal abstract class MessageQueueBase : IMessageQueue
    {
        public MessageQueueBase(
            IConfiguration configuration,
            IDatabase database,
            string queueKey,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IClientMasterData clientMasterData,
            IMemRepository<Guid, MessageDbo> masterMessages,
            IOprRepositoryGuid<MessageDbo> messageDbOpr)
        {
            this.configuration = configuration;
            this.database = database;
            this.queueKey = queueKey;
            this.matchType = matchType;
            this.sendType = sendType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.clientMasterData = clientMasterData;


            this.isSendRuning = false;

            this.messageDbOpr = messageDbOpr;

            this.clientChildData = ClientChildDataFactory.Create(
                        ClientChildDataFactoryEnum.Default,
                        clientMasterData,
                        queueKey);
        }

        protected IConfiguration configuration;
        protected IDatabase database;

        string distriburorKey;
        public string DistributorKey { get => distriburorKey; set => distriburorKey = value; }

        string queueKey;
        public string QueueKey { get => queueKey; set => queueKey = value; }

        MatchTypeEnum matchType;
        public MatchTypeEnum MatchType { get => matchType; set => matchType = value; }

        SendTypeEnum sendType;
        public SendTypeEnum SendType { get => sendType; set => sendType = value; }

        PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType { get => permissionType; set => permissionType = value; }

        public bool isDurable;
        public bool IsDurable { get => isDurable; set => isDurable = value; }

        protected bool isActive = true;
        public bool IsActive { get => isActive; set => isActive = value; }




        bool isSendRuning;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }

        readonly IClientMasterData clientMasterData;



        protected IClientChildData clientChildData;
        public  IClientChildData ClientChildData { get => clientChildData; set => clientChildData = value; }


        public abstract int ChildMessageCount { get; }

        protected int endConsumerNumber = 0;

        protected readonly IOprRepositoryGuid<MessageDbo> messageDbOpr;


        public abstract Task<ResponseModel> Queueing(MessageDbo message);
        protected abstract void Qperation();
    }
}
