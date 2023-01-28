using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using JoberMQ.Common.Models.DeclareConsume;
using System.Collections.Concurrent;

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



        public async Task<bool> DeclareConsume(string declareConsumeBuilder)
        {
            //todo buradayım
            var data = JsonConvert.DeserializeObject<ConcurrentDictionary<string, DeclareConsumeModel>>(declareConsumeBuilder);
            var client = JoberHost.Jober.ClientMaster.Get(Context.ConnectionId);
            client.DeclareConsuming = data;

            JoberHost.Jober.ClientMaster.Update(Context.ConnectionId, client);
            return true;
        }
    }
}
