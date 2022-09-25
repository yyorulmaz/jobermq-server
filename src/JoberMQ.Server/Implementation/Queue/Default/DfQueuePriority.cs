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
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace JoberMQ.Server.Implementation.Queue.Default
{
    internal class DfQueuePriority : QueueBase
    {
        IQueueChildDataBasePriority ChildData;

        public DfQueuePriority(BrokerConfigModel brokerConfig, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable, IClientGroup clientGroup, IQueueDataBase queueDataBase, IMessageDbOpr messageDbOpr) : base(brokerConfig, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, queueDataBase, messageDbOpr)
        {
            ChildData = QueueChildDataBaseFactory.CreateQueueChildDataBasePriority(brokerConfig.QueueChildPriorityFactory, queueDataBase);

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

        public override JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            var add = ChildData.Add(message);

            var result = new JobDataAddResponseModel();
            result.IsOnline = true;
            result.IsSuccess = add;
            result.JobId = message.Id;

            return result;
        }

        protected override void Qperation()
        {
            foreach (var message in ChildData.Data.OrderByDescending(x => x.Value.PriorityType))
            {
                IClient client;

                if (MatchType == MatchTypeEnum.Special)
                    client = ClientGroup.Get(x => x.ClientKey == message.Value.ConsumerKey);
                else
                    client = ClientGroup.Get(x => x.RowNumber > endConsumerNumber);

                if (client != null)
                {
                    Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
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
