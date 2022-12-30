using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Factories;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfMessageQueuePriority<THub> : MessageQueueBase where THub : Hub
    {
        IChildMemGeneralRepository<Guid, MessageDbo> ChildData;
        IHubContext<THub> context;
        public DfMessageQueuePriority(IConfigurationQueue configurationQueue, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable, IClientGroup clientGroup, IMemRepository<Guid, MessageDbo> queueDataBase, IMessageDbOpr messageDbOpr, IHubContext<THub> context) : base(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, queueDataBase, messageDbOpr)
        {
            ChildData = QueueChildDataBaseFactory.CreateQueueChildDataBasePriority(configurationQueue.QueueChildPriorityFactory, queueDataBase);
            this.context = context;

            clientGroup.ChangedAdded += ClientGroup_ChangedAdded;
            clientGroup.ChangedUpdated += ClientGroup_ChangedUpdated;
            ChildData.ChangedAdded += Messages_ChangedAdded;
        }

        private void ClientGroup_ChangedAdded(IClient obj) => SendOperation();
        private void ClientGroup_ChangedUpdated(IClient obj) => SendOperation();
        private void Messages_ChangedAdded(MessageDbo obj) => SendOperation();
        private void SendOperation()
        {
            if (IsSendRuning == false && ChildData.Count > 0)
            {
                IsSendRuning = true;
                Qperation();
            }
        }

        public override bool QueueAdd(MessageDbo message)
            => ChildData.Add(message.Id, message);

        protected override void Qperation()
        {
            foreach (var message in ChildData.ChildData.OrderByDescending(x => x.Value.PriorityType))
            {
                IClient client;

                if (MatchType == MatchTypeEnum.Special)
                    client = ClientGroup.Get(x => x.ClientKey == message.Value.ConsumerKey);
                else
                    client = ClientGroup.Get(x => x.RowNumber > endConsumerNumber);

                if (client != null)
                {
                    //Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    context.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    message.Value.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    messageDbOpr.Update(message.Value);

                    ChildData.Remove(message.Value.Id);

                    endConsumerNumber = client.RowNumber;
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
