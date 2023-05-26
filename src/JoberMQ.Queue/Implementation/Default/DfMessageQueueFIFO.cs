using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Response;
using JoberMQ.State;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfMessageQueueFIFO<THub> : MessageQueueBase
     where THub : Hub
    {
        IHubContext<THub> hubContext;
        IMemChildFIFORepository<Guid, MessageDbo> messageChilds { get; set; }

        public override int ChildMessageCount => messageChilds.Count;

        public DfMessageQueueFIFO(IConfiguration configuration, IDatabase database, string queueKey, QueueMatchTypeEnum matchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, IClientMasterData clientMasterData, IMemRepository<Guid, MessageDbo> masterMessages, IOprRepositoryGuid<MessageDbo> messageDbOpr, ref IHubContext<THub> hubContext) : base(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr)
        {
            messageChilds = MemChildFactory.CreateChildFIFO<Guid, MessageDbo>(Library.Database.Enums.MemChildFactoryEnum.Default, masterMessages);
            this.hubContext = hubContext;
            
            this.clientChildData.ChangedAdded += ClientChilds_ChangedAdded;
            this.clientChildData.ChangedUpdated += ClientChilds_ChangedUpdated;
            messageChilds.ChangedAdded += MessageChilds_ChangedAdded;
        }


        private void ClientChilds_ChangedAdded(string arg1, IClient arg2) => SendOperation();
        private void ClientChilds_ChangedUpdated(string arg1, IClient arg2) => SendOperation();
        private void MessageChilds_ChangedAdded(Guid arg1, MessageDbo arg2) => SendOperation();
        private void SendOperation()
        {
            if (IsSendRuning == false && messageChilds.Count > 0 && JoberMQState.IsJoberActive == true)
            {
                IsSendRuning = true;

                Task.Run(() => { 
                    Qperation();
                });
            }
        }

        public override async Task<ResponseModel> Queueing(MessageDbo message)
        {
            var result = new ResponseModel();
            result.IsOnline = true;


            var msgAdd = database.Message.Add(message.Id, message);
            if (msgAdd)
            {
                var msgChildAdd = messageChilds.Add(message.Id, message);
                if (msgChildAdd)
                    result.IsSucces = true;
                else
                {
                    database.Message.Delete(message.Id, message);
                    result.IsSucces = false;
                }
            }
            else
            {
                result.IsSucces = false;
            }

            return result;
        }

        protected override void Qperation()
        {
            Task.Run(() =>
            {
                while (messageChilds.ChildData != null && messageChilds.ChildData.Count > 0)
                {
                        var message = messageChilds.Get();
                        IClient client;


                        //todo burada group olma durumunda ne olacak düşün
                        if (MatchType == QueueMatchTypeEnum.Special)
                            client = ClientChildData.Get(x => x.ClientKey == message.Message.Routing.ClientKey);
                        else
                            client = ClientChildData.Get(x => x.Number > endConsumerNumber);


                        if (client != null)
                        {

                            hubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                            message.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                            messageDbOpr.Update(message.Id, message);

                            messageChilds.Remove(message.Id);

                            endConsumerNumber = client.Number;
                        }
                        else
                        {
                            // todo mesajın denenme durumlarına göre operasyonlar
                        }
                }

                IsSendRuning = false;

            });
        }
    }
}
