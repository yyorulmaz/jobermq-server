using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;

namespace JoberMQ.Implementation.Queue.Default
{
    internal class DefaultMessageQueueAllFIFO : MessageQueueBase
    {
        IMemChildFIFORepository<Guid, MessageDbo> messageChilds { get; set; }
        public DefaultMessageQueueAllFIFO(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault, bool isActive) : base(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive)
        {
            messageChilds = MemChildFactory.CreateChildFIFO<Guid, MessageDbo>(MemChildFactoryEnum.Default, JoberHost.JoberMQ.MessageMasterData);
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
            Task.Run(() =>
            {
                while (messageChilds.ChildData != null && messageChilds.ChildData.Count > 0)
                {
                    var message = messageChilds.Get();

                    //todo paralel for kullanırmıyım
                    foreach (var client in clientChildData)
                    {
                        //JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.Value.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                        JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.Value.ConnectionId).SendCoreAsync("ReceiveData", new[] { message }).ConfigureAwait(false);
                        message.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                        JoberHost.JoberMQ.Database.Message.Update(message.Id, message);

                        messageChilds.Remove(message.Id);
                    }
                }

                IsSendRuning = false;

            });
        }
    }
}
