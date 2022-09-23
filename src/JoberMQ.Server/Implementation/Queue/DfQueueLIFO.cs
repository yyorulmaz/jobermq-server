using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Enums.Status;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Factories.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using Newtonsoft.Json;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class DfQueueLIFO : QueueBase
    {
        IQueueChildDataBaseLIFO ChildData;

        public DfQueueLIFO(BrokerConfigModel brokerConfig, string distributorName, string queueName, MatchTypeEnum matchType, SendTypeEnum sendType, IClientGroup clientGroup, IQueueDataBase queueDataBase, IMessageDbOpr messageDbOpr) : base(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, queueDataBase, messageDbOpr)
        {
            ChildData = QueueChildDataBaseFactory.CreateQueueChildDataBaseLIFO(brokerConfig.QueueChildLIFOFactory, queueDataBase);

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
            while (ChildData.Data != null)
            {
                var message = ChildData.Get();
                IClient client;

                if (MatchType == MatchTypeEnum.ClientKey)
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
