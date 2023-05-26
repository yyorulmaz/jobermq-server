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
    internal class DefaultMessageQueueClientKeyPriority : MessageQueueBase
    {
        IMemChildGeneralRepository<Guid, MessageDbo> messageChilds { get; set; }
        public DefaultMessageQueueClientKeyPriority(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault, bool isActive) : base(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive)
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
                //var client = JoberHost.JoberMQ.Clients.Get(x => x.ClientKey == message.Value.Message.Routing.ClientKey);
                var client = clientChildData.FirstOrDefault(x => x.Value.ClientKey == message.Value.Message.Routing.ClientKey);

                if (client.Value != null)
                {
                    //JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.Key).SendCoreAsync("ReceiveData", new[] { message.Value }).ConfigureAwait(false);
                    message.Value.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    JoberHost.JoberMQ.Database.Message.Update(message.Key, message.Value);

                    messageChilds.Remove(message.Value.Id);
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
