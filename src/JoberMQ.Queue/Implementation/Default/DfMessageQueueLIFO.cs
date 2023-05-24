using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Enums.Status;
using JoberMQ.Library.Models.Response;
using JoberMQ.State;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfMessageQueueLIFO<THub> : MessageQueueBase
         where THub : Hub
    {
        IMemChildLIFORepository<Guid, MessageDbo> messageChilds { get; set; }
        IHubContext<THub> hubContext;


        public override int ChildMessageCount => messageChilds.Count;

        public DfMessageQueueLIFO(IConfiguration configuration, IDatabase database, string queueKey, QueueMatchTypeEnum matchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, IClientMasterData clientMasterData, IMemRepository<Guid, MessageDbo> masterMessages, IOprRepositoryGuid<MessageDbo> messageDbOpr, ref IHubContext<THub> hubContext) : base(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr)
        {
            messageChilds = MemChildFactory.CreateChildLIFO<Guid, MessageDbo>(Library.Database.Enums.MemChildFactoryEnum.Default, masterMessages);
            this.hubContext = hubContext;

            this.ClientChildData.ChangedAdded += ClientChilds_ChangedAdded;
            this.ClientChildData.ChangedUpdated += ClientChilds_ChangedUpdated;
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
            while (messageChilds.ChildData != null)
            {
                var message = messageChilds.Get();
                IClient client;


                if (MatchType == QueueMatchTypeEnum.Special)
                    client = ClientChildData.Get(x => x.ClientKey == message.Message.Routing.ClientKey);
                else
                    client = ClientChildData.Get(x => x.Number > endConsumerNumber);


                if (client != null)
                {
                    //Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
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
        }
    }
}
