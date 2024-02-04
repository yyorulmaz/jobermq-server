using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Response;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using JoberMQ.Abstraction.Client;

namespace JoberMQ.Implementation.Queue.Default
{
    internal class DefaultMessageQueueAllPriority : MessageQueueBase
    {
        IMemChildGeneralRepository<Guid, MessageDbo> messageChilds { get; set; }
        public DefaultMessageQueueAllPriority(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault, bool isActive) : base(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive)
        {
            messageChilds = MemChildFactory.CreateChildGeneral<Guid, MessageDbo>(MemChildFactoryEnum.Default, JoberHost.JoberMQ.MessageMasterData);
            messageChilds.ChangedAdded += MessageChilds_ChangedAdded;
            SendOperation();
        }

        private void MessageChilds_ChangedAdded(Guid arg1, MessageDbo arg2) => SendOperation();
        protected override void SendOperation()
        {
            if (IsSendRuning == false && messageChilds.Count > 0 && JoberHost.IsJoberActive == true)
            {
                IsSendRuning = true;

                Task.Run(() => {
                    Qperation();
                });
            }
        }




        public override int ChildMessageCount => messageChilds.Count;

        protected override async Task<bool> ChildMessageAdd(MessageDbo message)
            => messageChilds.Add(message.Id, message);

        protected override void Qperation()
        {
            foreach (var message in messageChilds.ChildData.OrderByDescending(x => x.Value.Message.PriorityType))
            {
                //todo paralel for kullanırmıyım
                foreach (var client in clientChildData)
                    JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.Value.ConnectionId).SendCoreAsync("ReceiveMessage", new[] { message.Value }).ConfigureAwait(false);
                    
                MessageEndOperation(message.Value);
            }

            IsSendRuning = false;
        }

        private void MessageEndOperation(MessageDbo message)
        {
            //JoberHost.JoberMQ.Database.Message.Delete(message.Id, message);
            //messageChilds.Remove(message.Id);



            message.Message.MessageConsuming.ConsumingRetryCounter++;
            message.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;

            if (message.Message.MessageConsuming.ConsumingRetryCounter == message.Message.MessageConsuming.ConsumingRetryMaxCount && message.IsResult == false)
            {
                JoberHost.JoberMQ.Database.Message.Delete(message.Id, message);
                messageChilds.Remove(message.Id);
            }
            else if (message.Message.MessageConsuming.ConsumingRetryCounter == message.Message.MessageConsuming.ConsumingRetryMaxCount && message.IsResult == true)
            {
                JoberHost.JoberMQ.Database.Message.Delete(message.Id, message);
            }
            else
            {
                messageChilds.Remove(message.Id);
                messageChilds.Add(message.Id, message);
                JoberHost.JoberMQ.Database.Message.Update(message.Id, message);
            }
        }
    }
}
