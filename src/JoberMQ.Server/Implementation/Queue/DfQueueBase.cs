using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using System;

namespace JoberMQ.Server.Implementation.Queue
{
    internal abstract class DfQueueBase : IQueue
    {
        private readonly string distributorName;
        private readonly string queueName;
        private readonly MatchTypeEnum matchType;
        private readonly SendTypeEnum sendType;
        private readonly IClientGroup clientGroup;
        private readonly IConcurrentDictionaryChildRepository<Guid, MessageDbo> messages;
        private bool isSendRuning = false;
        public DfQueueBase(string distributorName, string queueName, MatchTypeEnum matchType, SendTypeEnum sendType, IClientGroup clientGroup, IConcurrentDictionaryRepository<Guid, MessageDbo> mainQueue)
        {
            this.distributorName = distributorName;
            this.queueName = queueName;
            this.matchType = matchType;
            this.sendType = sendType;
            this.clientGroup = clientGroup;
            this.messages = new ConcurrentDictionaryChildRepository<Guid, MessageDbo>(mainQueue);

            this.clientGroup.ChangedAdded += ClientGroup_ChangedAdded;
            this.clientGroup.ChangedUpdated += ClientGroup_ChangedUpdated;
            this.messages.ChangedAdded += Messages_ChangedAdded;
        }


        public string DistributorName => distributorName;
        public string QueueName => queueName;
        public MatchTypeEnum MatchType => matchType;
        public SendTypeEnum SendType => sendType;
        public IClientGroup ClientGroup => clientGroup;
        public IConcurrentDictionaryChildRepository<Guid, MessageDbo> Messages => messages;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }


        private void SendOperation()
        {
            if (IsSendRuning == false && Messages.Count > 0)
            {
                IsSendRuning = true;
                switch (matchType)
                {
                    case MatchTypeEnum.ClientKey:
                        switch (sendType)
                        {
                            case SendTypeEnum.Priority:
                                Qperation_ClientKey_Priority();
                                break;
                            case SendTypeEnum.FIFO:
                                Qperation_ClientKey_FIFO();
                                break;
                            case SendTypeEnum.LIFO:
                                Qperation_ClientKey_LIFO();
                                break;
                        }
                        break;
                    case MatchTypeEnum.ClientGroupKey:
                        switch (sendType)
                        {
                            case SendTypeEnum.Priority:
                                Qperation_ClientGroupKey_Priority();
                                break;
                            case SendTypeEnum.FIFO:
                                Qperation_ClientGroupKey_FIFO();
                                break;
                            case SendTypeEnum.LIFO:
                                Qperation_ClientGroupKey_LIFO();
                                break;
                        }
                        break;
                }
            }
        }
        private void ClientGroup_ChangedAdded(IClient obj) => SendOperation();
        private void ClientGroup_ChangedUpdated(IClient obj) => SendOperation();
        private void Messages_ChangedAdded(MessageDbo obj) => SendOperation();


        protected abstract void Qperation_ClientKey_Priority();
        protected abstract void Qperation_ClientKey_FIFO();
        protected abstract void Qperation_ClientKey_LIFO();
        protected abstract void Qperation_ClientGroupKey_Priority();
        protected abstract void Qperation_ClientGroupKey_FIFO();
        protected abstract void Qperation_ClientGroupKey_LIFO();

        public JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            var messageAdd = messages.Add(message.Id, message);
            var response = new JobDataAddResponseModel();
            response.IsOnline = true;
            response.IsSuccess = messageAdd;
            response.JobId = message.Id;

            return response;
        }
    }
}
