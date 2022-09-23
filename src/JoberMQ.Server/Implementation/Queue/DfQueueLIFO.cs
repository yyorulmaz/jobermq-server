using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Factories.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class DfQueueLIFO : QueueBase
    {
        IQueueChildDataBaseLIFO Data;

        public DfQueueLIFO(BrokerConfigModel brokerConfig, string distributorName, string queueName, MatchTypeEnum matchType, SendTypeEnum sendType, IClientGroup clientGroup, IQueueDataBase queueDataBase) : base(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, queueDataBase)
        {
            Data = QueueChildDataBaseFactory.CreateQueueChildDataBaseLIFO(brokerConfig.QueueChildLIFOFactory, queueDataBase);

            clientGroup.ChangedAdded += ClientGroup_ChangedAdded;
            clientGroup.ChangedUpdated += ClientGroup_ChangedUpdated;
            Data.ChangedAdded += Messages_ChangedAdded;
        }

        private void ClientGroup_ChangedAdded(IClient obj) => SendOperation();
        private void ClientGroup_ChangedUpdated(IClient obj) => SendOperation();
        private void Messages_ChangedAdded(MessageDbo obj) => SendOperation();
        private void SendOperation()
        {
            if (IsSendRuning == false && Data.Count > 0)
            {
                IsSendRuning = true;
                Qperation();
            }
        }

        public override JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            var add = Data.Add(message);

            var result = new JobDataAddResponseModel();
            result.IsOnline = true;
            result.IsSuccess = add;
            result.JobId = message.Id;

            return result;
        }

        protected override void Qperation()
        {

            IsSendRuning = false;
        }
    }
}
