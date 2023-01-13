using JoberMQ.Common.Enums.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using System;
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
            var clientKey = Context.GetHttpContext()?.Request.Headers["ClientId"].ToString();
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










        internal async Task<bool> Job()
        {

            return true;
        }
        internal async Task<bool> Message()
        {


            return true;
        }
        internal async Task<bool> Rpc()
        {


            return true;
        }
    }
}
