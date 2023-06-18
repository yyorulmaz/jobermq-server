using JoberMQ.Common;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Distributor;
using JoberMQ.Common.Models.Queue;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Rpc;
using JoberMQ.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JoberMQ.Hubs
{

    public class JoberHub : Hub
    {
        #region Connect
        public override Task OnConnectedAsync()
        {
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
            var result = JoberHost.JoberMQ.DisconnectedOperationAsync(Context).Result;
            return base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region Distributor
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel<DistributorModel>> DistributorGet(string data)
            => await JoberHost.JoberMQ.DistributorOperationGetAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> DistributorCreate(DistributorModel data)
            => await JoberHost.JoberMQ.DistributorOperationCreateAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> DistributorEdit(DistributorModel data)
            => await JoberHost.JoberMQ.DistributorOperationEditAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> DistributorRemove(string data)
            => await JoberHost.JoberMQ.DistributorOperationRemoveAsync(data);
        #endregion

        #region Queue
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel<QueueModel>> QueueGet(string data)
            => await JoberHost.JoberMQ.QueueOperationGetAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueCreate(QueueModel data)
            => await JoberHost.JoberMQ.QueueOperationCreateAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueEdit(QueueModel data)
            => await JoberHost.JoberMQ.QueueOperationEditAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueRemove(string data)
            => await JoberHost.JoberMQ.QueueOperationRemoveAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> QueueBind(string data)
            => await JoberHost.JoberMQ.QueueOperationBindAsync(data);
        #endregion

        #region Consume
        // todo başlangıçta birkaçtane yetki gruplarına göre kullanıcı ekle
        //[Authorize(Roles = "user")]
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> ConsumeSub(string clientKey, string queueKey, bool isDurable)
        {
            var result = await JoberHost.JoberMQ.ConsumeOperationSubAsync(clientKey, queueKey, isDurable);
            JoberHost.JoberMQ.Clients.InvokeChangedAdded(Context.ConnectionId);
            return result;
        }
        //[Authorize(Roles = "user")]
        [Authorize(Roles = "administrators")]
        public async Task<ResponseBaseModel> ConsumeUnSub(string clientKey, string queueKey)
        {
            var result = await JoberHost.JoberMQ.ConsumeOperationUnSubAsync(clientKey, queueKey);
            JoberHost.JoberMQ.Clients.InvokeChangedRemoved(Context.ConnectionId);
            return result;
        }
        #endregion

        #region Message
        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Message(MessageDbo data)
            => await JoberHost.JoberMQ.MessageOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Job(string data)
            => await JoberHost.JoberMQ.JobOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task<RpcResponseModel> Rpc(string data)
            => await JoberHost.JoberMQ.RpcOperationAsync(data);
        [Authorize(Roles = "administrators")]
        public async Task RpcResponse(string rpc)
            => await JoberHost.JoberMQ.RpcResponseOperationAsync(rpc);
        #endregion









        public async Task<List<StatusSSSSModel>> StatusGet(string data)
        {
            var result = new List<StatusSSSSModel>();

            result.Add(new StatusSSSSModel
            {
                Name = "DistributorMemData.DistributorDatas.Count",
                Value = DistributorMemData.DistributorDatas.Count.ToString(),   
            });
            result.Add(new StatusSSSSModel
            {
                Name = "InMemoryMessage.MessageMasterData.Count",
                Value = InMemoryMessage.MessageMasterData.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "JobMemData.JobDatas.Count",
                Value = JobMemData.JobDatas.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "JobTransactionMemData.JobTransactionDatas.Count",
                Value = JobTransactionMemData.JobTransactionDatas.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "MessageMemData.MessageDatas.Count",
                Value = MessageMemData.MessageDatas.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "MessageResultMemData.MessageResultDatas.Count",
                Value = MessageResultMemData.MessageResultDatas.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "QueueMemData.QueueDatas.Count",
                Value = QueueMemData.QueueDatas.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "SubscriptMemData.SubscriptDatas.Count",
                Value = SubscriptMemData.SubscriptDatas.Count.ToString(),
            });
            result.Add(new StatusSSSSModel
            {
                Name = "UserMemData.UserDatas.Count",
                Value = UserMemData.UserDatas.Count.ToString(),
            });



            result.Add(new StatusSSSSModel
            {
                Name = "JoberHost.IsJoberActive",
                Value = JoberHost.IsJoberActive.ToString(),
            });



            result.Add(new StatusSSSSModel
            {
                Name = "JoberHost.JoberMQ.MessageMasterData.Count",
                Value = JoberHost.JoberMQ.MessageMasterData.Count.ToString(),
            });



            foreach (var item in JoberHost.JoberMQ.Queues.MasterData)
            {
                if (item.Value.ChildMessageCount > 0)
                {

                    result.Add(new StatusSSSSModel
                    {
                        Name = item.Value.QueueKey,
                        Value = item.Value.ChildMessageCount.ToString(),
                    });
                }
            }

            result.Add(new StatusSSSSModel
            {
                Name = "JoberHost.JoberMQ.Clients.Count",
                Value = JoberHost.JoberMQ.Clients.Count.ToString(),
            });



            return result;
        }
    }

    
}
