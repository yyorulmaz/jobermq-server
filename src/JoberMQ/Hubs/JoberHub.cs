using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Distributor;
using JoberMQ.Common.Models.Queue;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Rpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQ.Hubs
{

    public class JoberHub : Hub
    {
        #region Connect
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Clients.Count Connect : " + JoberHost.JoberMQ.Clients.Count);


            var result = JoberHost.JoberMQ.ConnectedOperationAsync(Context).Result;
            if (result == false)
            {
                var errorMessage = JoberHost.JoberMQ.StatusCode.GetStatusMessage("0.0.14");
                Console.WriteLine(errorMessage);
                return base.OnDisconnectedAsync(new Exception(errorMessage));
            }
            else
            {
                return base.OnConnectedAsync();
            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Clients.Count Disconnect : " + JoberHost.JoberMQ.Clients.Count);

            var result = JoberHost.JoberMQ.DisconnectedOperationAsync(Context).Result;
            return base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region Distributor
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> DistributorAdd(DistributorModel data)
            => await JoberHost.JoberMQ.DistributorAddOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel<DistributorModel>> DistributorGet(string data)
            => await JoberHost.JoberMQ.DistributorGetOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> DistributorEdit(DistributorModel data)
            => await JoberHost.JoberMQ.DistributorEditOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> DistributorRemove(string data)
            => await JoberHost.JoberMQ.DistributorRemoveOperationAsync(data);
        #endregion

        #region Queue
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel<QueueModel>> QueueGet(string data)
            => await JoberHost.JoberMQ.QueueGetOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel<List<QueueModel>>> QueueGetAll(string data)
           => await JoberHost.JoberMQ.QueueGetAllOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueAdd(QueueModel data)
            => await JoberHost.JoberMQ.QueueAddOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueEdit(QueueModel data)
            => await JoberHost.JoberMQ.QueueEditOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueRemove(string data)
            => await JoberHost.JoberMQ.QueueRemoveOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueBind(string data)
            => await JoberHost.JoberMQ.QueueBindOperationAsync(data);
        #endregion

        #region Consume
        // todo başlangıçta birkaçtane yetki gruplarına göre kullanıcı ekle
        //[Authorize(Roles = "user")]
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> ConsumeSub(string clientKey, string queueKey, bool isDurable)
        {
            var result = await JoberHost.JoberMQ.ConsumeSubOperationAsync(clientKey, queueKey, isDurable);
            JoberHost.JoberMQ.Clients.InvokeChangedAdded(Context.ConnectionId);
            return result;
        }
        //[Authorize(Roles = "user")]
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> ConsumeUnSub(string clientKey, string queueKey)
        {
            var result = await JoberHost.JoberMQ.ConsumeUnSubOperationAsync(clientKey, queueKey);
            JoberHost.JoberMQ.Clients.InvokeChangedRemoved(Context.ConnectionId);
            return result;
        }


        [Authorize(Roles = "administrators")]
        public async Task ConsumeSubFreeMessageGroup(string groupKey)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupKey);
        }
        [Authorize(Roles = "administrators")]
        public async Task ConsumeUnSubFreeMessageGroup(string groupKey)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupKey);
        }
        #endregion

        #region Job
        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Job(JobDbo data)
            => await JoberHost.JoberMQ.JobOperationAsync(data);
        #endregion

        #region Message
        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Message(MessageDbo data)
           => await JoberHost.JoberMQ.MessageOperationAsync(data);
        #endregion

        #region Rpc
        [Authorize(Roles = "administrators")]
        public async Task<RpcResponseModel> Rpc(string data)
            => await JoberHost.JoberMQ.RpcOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task RpcResponse(string rpc)
            => await JoberHost.JoberMQ.RpcResponseOperationAsync(rpc);
        #endregion

        #region FreeMessage
        [Authorize(Roles = "administrators")]
        public async Task FreeMessageToClient(string message, string clientKey)
        {
            var client = JoberHost.JoberMQ.Clients.Get(x => x.ClientKey == clientKey);
            if (client != null)
                _ = Clients.Client(client.ConnectionId).SendAsync("ReceiveDataText", message);
        }
        [Authorize(Roles = "administrators")]
        public async Task FreeMessageToGroup(string message, string groupKey)
        {
            _ = Clients.Group(groupKey).SendAsync("ReceiveDataText", message);
        }




        
        #endregion

    }
}
