using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Enums.Status;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Factories.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace JoberMQ.Server.Implementation.Queue.Default
{
    internal class DfQueueFIFO : QueueBase
    {
        IDbChildFIFO ChildData;

        public DfQueueFIFO(BrokerConfigModel brokerConfig, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable, IClientGroup clientGroup, IDb queueDataBase, IMessageDbOpr messageDbOpr) : base(brokerConfig, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, queueDataBase, messageDbOpr)
        {
            ChildData = QueueChildDataBaseFactory.CreateQueueChildDataBaseFIFO(brokerConfig.QueueChildFIFOFactory, queueDataBase);

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
            => ChildData.Add(message);

        protected override void Qperation()
        {
            while (ChildData.Data != null)
            {
                var message = ChildData.Get();
                IClient client;

                if (MatchType == MatchTypeEnum.Special)
                    client = ClientGroup.Get(x => x.ClientKey == message.ConsumerKey);
                else
                    client = ClientGroup.Get(x => x.RowNumber > endConsumerNumber);

                if (client != null)
                {
                    Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                    message.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    messageDbOpr.Update(message);

                    ChildData.Remove(message.Id);

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
