using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfMessageQueuePriority<THub> : MessageQueueBase
      where THub : Hub
    {
        IMemChildGeneralRepository<Guid, MessageDbo> MessageChilds { get; set; }
        IHubContext<THub> hubContext;
        IMemChildToolsRepository<string, IClient> clientChilds;
        public override IMemChildToolsRepository<string, IClient> ClientChilds { get => clientChilds; set => clientChilds = value; }

        public DfMessageQueuePriority(string distriburorKey, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IClient> masterClient, IMemRepository<Guid, MessageDbo> masterQueue, IOprRepositoryGuid<MessageDbo> messageDbOpr, ref bool isJoberActive, IHubContext<THub> hubContext) : base(distriburorKey, queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterQueue, messageDbOpr, ref isJoberActive)
        {
            MessageChilds = MemChildFactory.CreateChildGeneral<Guid, MessageDbo>(Library.Database.Enums.MemChildFactoryEnum.Default, masterQueue);
            this.hubContext = hubContext;


            //x => x.IsConsumeSpecial == true
            //x => x.IsConsumer == true && x.IsConsumeSpecial == true && x.RowNumber > 0
            //k => k.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.SpecialAdd) != null);

            switch (matchType)
            {
                case MatchTypeEnum.Special:
                    this.clientChilds = MemChildFactory.CreateChildTools<string, IClient>(
                        MemChildFactoryEnum.Default,
                        masterClient,
                        true,
                        x => x.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.Special) != null,
                        true,
                        x => x.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.Special) != null,
                        true,
                        x => x.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.Special) != null,
                        false,
                        null,
                        false,
                        null,
                        false,
                        null);
                    break;
                case MatchTypeEnum.Group:
                    this.clientChilds = MemChildFactory.CreateChildTools<string, IClient>(
                        MemChildFactoryEnum.Default,
                        masterClient,
                        true,
                        x => x.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.Group && w.Value.DeclareKey == this.QueueKey) != null,
                        true,
                        x => x.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.Group && w.Value.DeclareKey == this.QueueKey) != null,
                        true,
                        x => x.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == Common.Enums.DeclareConsume.DeclareConsumeTypeEnum.Group && w.Value.DeclareKey == this.QueueKey) != null,
                        false,
                        null,
                        false,
                        null,
                        false,
                        null);
                    break;
                case MatchTypeEnum.Free:
                    //todo yap

                    break;
                default:
                    break;
            }

            

            this.ClientChilds.ChangedAdded += ClientChilds_ChangedAdded;
            this.ClientChilds.ChangedUpdated += ClientChilds_ChangedUpdated;
            MessageChilds.ChangedAdded += MessageChilds_ChangedAdded;
        }

        private void ClientChilds_ChangedAdded(string arg1, IClient arg2) => SendOperation();
        private void ClientChilds_ChangedUpdated(string arg1, IClient arg2) => SendOperation();
        private void MessageChilds_ChangedAdded(Guid arg1, MessageDbo arg2) => SendOperation();
        private void SendOperation()
        {
            if (IsSendRuning == false && MessageChilds.Count > 0 && isJoberActive == true)
            {
                IsSendRuning = true;
                Qperation();
            }
        }

        public override bool MessageAdd(MessageDbo message)
            => MessageChilds.Add(message.Id, message);

        protected override void Qperation()
        {
            foreach (var message in MessageChilds.ChildData.OrderByDescending(x => x.Value.Message.PriorityType))
            {
                IClient client;

                if (MatchType == MatchTypeEnum.Special)
                    client = ClientChilds.Get(x => x.ClientKey == message.Value.Consuming.ClientKey);
                else
                    client = ClientChilds.Get(x => x.Number > endConsumerNumber);

                if (client != null)
                {
                    //Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    hubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    message.Value.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    messageDbOpr.Update(message.Key, message.Value);

                    MessageChilds.Remove(message.Value.Id);

                    endConsumerNumber = client.Number;
                }
                else
                {
                    // todo mesajın denenme durumlarına göre operasyonlar
                }
            }

            IsSendRuning = false;
        }
    }
}
