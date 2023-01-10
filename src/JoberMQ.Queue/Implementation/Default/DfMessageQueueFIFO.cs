using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfMessageQueueFIFO<THub> : MessageQueueBase
     where THub : Hub
    {
        IMemChildFIFORepository<Guid, MessageDbo> MessageChilds { get; set; }
        IHubContext<THub> hubContext;

        public DfMessageQueueFIFO(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IClient> masterClient, IMemRepository<Guid, MessageDbo> masterQueue, IOprRepositoryGuid<MessageDbo> messageDbOpr, ref bool isJoberActive, IHubContext<THub> hubContext) : base(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterQueue, messageDbOpr, ref isJoberActive)
        {
            MessageChilds = MemChildFactory.CreateChildFIFO<Guid, MessageDbo>(Library.Database.Enums.MemChildFactoryEnum.Default, masterQueue);
            this.hubContext = hubContext;

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
            while (MessageChilds.ChildData != null)
            {
                var message = MessageChilds.Get();
                IClient client;

                if (MatchType == MatchTypeEnum.Special)
                    client = ClientChilds.Get(x => x.ClientKey == message.ConsumerKey);
                else
                    client = ClientChilds.Get(x => x.Number > endConsumerNumber);

                if (client != null)
                {
                    //Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                    hubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                    message.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    messageDbOpr.Update(message.Id, message);

                    MessageChilds.Remove(message.Id);

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
