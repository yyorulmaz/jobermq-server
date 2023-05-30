﻿using JoberMQ.Abstraction.Client;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using System.Text.RegularExpressions;

namespace JoberMQ.Implementation.Queue.Default
{
    internal class DefaultMessageQueueTagPriority : MessageQueueBase
    {
        IMemChildGeneralRepository<Guid, MessageDbo> messageChilds { get; set; }
        public DefaultMessageQueueTagPriority(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault, bool isActive) : base(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive)
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
                List<IClient> matchingModels = new List<IClient>();

                foreach (IClient cli in clientChildData.Values.ToArray())
                {
                    foreach (string tag in cli.Tags)
                    {
                        if (Regex.IsMatch(tag, message.Value.Message.Routing.ClientTag))
                        {
                            matchingModels.Add(cli);
                            break; // Eşleşme bulunduğunda döngüyü sonlandır
                        }
                    }
                }

                if (matchingModels == null || matchingModels.Count == 0)
                    continue;

                foreach (var client in matchingModels)
                {
                    //JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { message.Value }).ConfigureAwait(false);
                    message.Value.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    JoberHost.JoberMQ.Database.Message.Update(message.Key, message.Value);

                    messageChilds.Remove(message.Value.Id);
                }
            }

            IsSendRuning = false;
        }
    }
}