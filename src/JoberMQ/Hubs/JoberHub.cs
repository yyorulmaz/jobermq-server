using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.DeclareConsume;
using JoberMQ.Common.Models.Distributor;
using JoberMQ.Common.Models.Queue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace JoberMQ.Hubs
{
    internal class JoberHub : Hub
    {
        //todo CLIENT CONNECT OLDUĞUNDA CLIENTGROUPKEY İNE GÖRE KUYRUK OLUŞTURMA DURUMU

        #region CONNECT - DISCONNECT
        public override Task OnConnectedAsync()
        {
            var clientType = (ClientTypeEnum)Enum.Parse(typeof(ClientTypeEnum), Context.GetHttpContext()?.Request.Headers["ClientType"].ToString());
            var clientKey = Context.GetHttpContext()?.Request.Headers["ClientKey"].ToString();
            var clientGroupKey = Context.GetHttpContext()?.Request.Headers["ClientGroupKey"].ToString();
            var isOfflineClient = Convert.ToBoolean(Context.GetHttpContext()?.Request.Headers["IsOfflineClient"]);


            var client = JoberMQ.Client.Factories.ClientFactory.CreateClient(
                JoberHost.Jober.Configuration.ConfigurationClient.ClientFactory,
                Context.ConnectionId,
                clientKey,
                clientGroupKey,
                clientType);

            JoberHost.Jober.ClientMaster.Add(Context.ConnectionId, client);

            ////todo cluster
            //var highAvailabilities = Startup.ClientService.ClientData.GetAll(x => x.ClientType == ClientTypeEnum.HighAvailability || x.ClientType == ClientTypeEnum.LoadBalancingANDHighAvailability);
            //if (highAvailabilities == null || highAvailabilities.Count == 0)
            //    Startup.ServerService.IsHighAvailability = false;
            //else
            //    Startup.ServerService.IsHighAvailability = true;


            Clients.Client(Context.ConnectionId).SendCoreAsync("ReceiveServerActive", new object[] { JoberHost.Jober.IsJoberActive });

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            JoberHost.Jober.ClientMaster.Remove(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
        #endregion








        public async Task<bool> Job(string message)
        {
            Console.WriteLine(message);


            return true;
        }
        public async Task<bool> Message(string message)
        {
            return JoberHost.Jober.MessageBroker.MessageAdd(JsonConvert.DeserializeObject<MessageDbo>(message));
        }
        public async Task<bool> Rpc(string message)
        {


            return true;
        }



        public async Task<bool> Consume(string consumeData)
        {
            //todo buradayım
            var data = JsonConvert.DeserializeObject<ConcurrentDictionary<string, DeclareConsumeModel>>(consumeData);
            var client = JoberHost.Jober.ClientMaster.Get(Context.ConnectionId);
            client.DeclareConsuming = data;

            JoberHost.Jober.ClientMaster.Update(Context.ConnectionId, client);
            return true;
        }

        //[Authorize("ssssss")]
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> Distributor(string distributorData)
        {
            var result = new ResponseBaseModel();
            var data = JsonConvert.DeserializeObject<DeclareDistributorModel>(distributorData);

            switch (data.DeclareDistributorOperationType)
            {
                case DeclareDistributorOperationTypeEnum.Create:
                    result = JoberHost.Jober.MessageBroker.DistributorCreate(data.DistributorKey, data.DistributorType, data.PermissionType, data.IsDurable);
                    break;
                case DeclareDistributorOperationTypeEnum.Update:
                    result = JoberHost.Jober.MessageBroker.DistributorUpdate(data.DistributorKey, data.IsDurable);
                    break;
                case DeclareDistributorOperationTypeEnum.Remove:
                    result = JoberHost.Jober.MessageBroker.DistributorRemove(data.DistributorKey);
                    break;
            }

            return result;
        }
        public async Task<ResponseBaseModel> Queue(string queueData)
        {
            var result = new ResponseBaseModel();
            var data = JsonConvert.DeserializeObject<DeclareQueueModel>(queueData);
            


            switch (data.DeclareQueueOperationType)
            {
                case Common.Enums.Queue.DeclareQueueOperationTypeEnum.Create:
                    result = JoberHost.Jober.MessageBroker.QueueCreate(data.DistributorKey, data.QueueKey, data.MatchType, data.SendType, data.PermissionType, data.IsDurable);
                    break;
                case Common.Enums.Queue.DeclareQueueOperationTypeEnum.Update:
                    result = JoberHost.Jober.MessageBroker.QueueUpdate(data.QueueKey, data.MatchType, data.SendType, data.PermissionType, data.IsDurable);
                    break;
                case Common.Enums.Queue.DeclareQueueOperationTypeEnum.Remove:
                    result = JoberHost.Jober.MessageBroker.QueueRemove(data.QueueKey);
                    break;
                case Common.Enums.Queue.DeclareQueueOperationTypeEnum.DistributorBind:
                    result = JoberHost.Jober.MessageBroker.QueueBind(data.DistributorKey, data.QueueKey);
                    break;
            }

            return result;
        }
    }
}
