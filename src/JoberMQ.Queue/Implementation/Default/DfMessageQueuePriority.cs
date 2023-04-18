using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Factories;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Enums.Status;
using JoberMQ.Library.Models.Response;
using JoberMQ.State.Abstraction;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfMessageQueuePriority<THub> : MessageQueueBase
      where THub : Hub
    {
        IHubContext<THub> hubContext;

        public override int ChildMessageCount => messageChilds.Count;
        IMemChildGeneralRepository<Guid, MessageDbo> messageChilds { get; set; }

        public DfMessageQueuePriority(IConfiguration configuration, IDatabase database, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable, IClientMasterData clientMasterData, IMemRepository<Guid, MessageDbo> masterMessages, IOprRepositoryGuid<MessageDbo> messageDbOpr, ref IJoberState joberState, ref IHubContext<THub> hubContext) : base(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref joberState)
        {
            joberState.IsJoberActiveEvent += JoberState_IsJoberActiveEvent;

            messageChilds = MemChildFactory.CreateChildGeneral<Guid, MessageDbo>(MemChildFactoryEnum.Default, masterMessages);
            this.hubContext = hubContext;


            //x => x.IsConsumeSpecial == true
            //x => x.IsConsumer == true && x.IsConsumeSpecial == true && x.RowNumber > 0
            //k => k.DeclareConsuming.Where(w => w.Value.DeclareConsumeType == ConsumeTypeEnum.SpecialAdd) != null);

            //switch (matchType)
            //{
            //    case MatchTypeEnum.Special:
            //        this.clientChildData = ClientChildDataFactory.Create(
            //            ClientChildDataFactoryEnum.Default,
            //            clientMasterData,
            //            ConsumeTypeEnum.Special,
            //            queueKey);
            //        break;
            //    case MatchTypeEnum.Group:
            //        this.clientChildData = ClientChildDataFactory.Create(
            //           ClientChildDataFactoryEnum.Default,
            //           clientMasterData,
            //           ConsumeTypeEnum.Group,
            //           queueKey);
            //        break;
            //    case MatchTypeEnum.Free:
            //        //todo yap

            //        break;
            //    default:
            //        break;
            //}



            this.ClientChildData.ChangedAdded += ClientChildData_ChangedAdded;
            this.ClientChildData.ChangedUpdated += ClientChildData_ChangedUpdated;
            messageChilds.ChangedAdded += MessageChilds_ChangedAdded;
        }

        private void JoberState_IsJoberActiveEvent(bool obj)
        {
            isJoberActive = obj;
        }

        private void ClientChildData_ChangedAdded(string arg1, IClient arg2) => SendOperation();
        private void ClientChildData_ChangedUpdated(string arg1, IClient arg2) => SendOperation();
        private void MessageChilds_ChangedAdded(Guid arg1, MessageDbo arg2) => SendOperation();
        private void SendOperation()
        {
            if (IsSendRuning == false && messageChilds.Count > 0 && isJoberActive == true)
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
            foreach (var message in messageChilds.ChildData.OrderByDescending(x => x.Value.Message.PriorityType))
            {
                IClient client;

                if (MatchType == MatchTypeEnum.Special)
                    //client = ClientChildData.Get(x => x.ClientKey == message.Value.Consuming.ClientKey);
                    client = ClientChildData.Get(x => x.ClientKey == message.Value.Message.Routing.ClientKey);
                else
                    client = ClientChildData.Get(x => x.Number > endConsumerNumber);


                if (client != null)
                {
                    //Factory.Server.JoberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    hubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveData", new[] { JsonConvert.SerializeObject(message.Value) }).ConfigureAwait(false);
                    message.Value.Status.StatusTypeMessage = StatusTypeMessageEnum.SendClient;
                    messageDbOpr.Update(message.Key, message.Value);

                    messageChilds.Remove(message.Value.Id);

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
