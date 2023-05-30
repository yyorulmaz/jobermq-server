﻿using JoberMQ.Common.Database.Enums;
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
    internal class DefaultMessageQueueInOrderLIFO : MessageQueueBase
    {
        IMemChildLIFORepository<Guid, MessageDbo> messageChilds { get; set; }
        public DefaultMessageQueueInOrderLIFO(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault, bool isActive) : base(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive)
        {
            messageChilds = MemChildFactory.CreateChildLIFO<Guid, MessageDbo>(MemChildFactoryEnum.Default, JoberHost.JoberMQ.MessageMasterData);
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
            while (messageChilds.ChildData != null)
            {
                var message = messageChilds.Get();
                var client = clientChildData.FirstOrDefault(x => x.Value.Number > endConsumerNumber).Value;

                if (client != null)
                {
                    //JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                    JoberHost.JoberMQ.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { message }).ConfigureAwait(false);
                    message.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    JoberHost.JoberMQ.Database.Message.Update(message.Id, message);

                    messageChilds.Remove(message.Id);

                    endConsumerNumber = client.Number;
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