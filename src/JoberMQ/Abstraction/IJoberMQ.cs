using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Distributor;
using JoberMQ.Common.Models.Queue;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Rpc;
using JoberMQ.Common.StatusCode.Abstraction;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using JoberMQ.Abstraction.Broker;
using JoberMQ.Abstraction.Client;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Database;
using JoberMQ.Abstraction.Distributor;
using JoberMQ.Abstraction.Queue;
using JoberMQ.Abstraction.Timing;
using JoberMQ.Hubs;
using System.Data.SqlTypes;

namespace JoberMQ.Abstraction
{
    public interface IJoberMQ
    {
        public JoberMQ.Abstraction.Configuration.IConfiguration Configuration { get; }
        public IStatusCode StatusCode { get; }
        public IClients Clients { get; }
        public IHubContext<JoberHub> JoberHubContext { get; set; }
        internal IMemRepository<Guid, MessageDbo> MessageMasterData { get; }
        public IDatabase Database { get; }
        internal ISchedule Schedule { get; }
        internal IMessageBroker MessageBroker { get; }
        internal IMemRepository<string, IMessageDistributor> Distributors { get; }
        internal IMemRepository<string, IMessageQueue> Queues { get; }

        public IScheduledNotifications ScheduledNotifications { get; }

        Task StartAsync(bool owinHost, IHubContext<JoberHub> hubContext = null);





        #region Connect
        public Task<bool> ConnectedOperationAsync(HubCallerContext context);
        public Task<bool> DisconnectedOperationAsync(HubCallerContext context);
        #endregion


        #region Distributor
        public Task<ResponseBaseModel<DistributorModel>> DistributorGetOperationAsync(string data);
        public Task<ResponseBaseModel> DistributorAddOperationAsync(DistributorModel data);
        public Task<ResponseBaseModel> DistributorEditOperationAsync(DistributorModel data);
        public Task<ResponseBaseModel> DistributorRemoveOperationAsync(string data);
        #endregion


        #region Queue
        public Task<ResponseBaseModel<QueueModel>> QueueGetOperationAsync(string data);
        public Task<ResponseBaseModel<List<QueueModel>>> QueueGetAllOperationAsync(string data);
        public Task<ResponseBaseModel> QueueAddOperationAsync(QueueModel data);
        public Task<ResponseBaseModel> QueueEditOperationAsync(QueueModel data);
        public Task<ResponseBaseModel> QueueRemoveOperationAsync(string data);
        public Task<ResponseBaseModel> QueueBindOperationAsync(string data);
        #endregion


        #region Consume
        public Task<ResponseBaseModel> ConsumeSubOperationAsync(string clientKey, string queueKey, bool isDurable);
        public Task<ResponseBaseModel> ConsumeUnSubOperationAsync(string clientKey, string queueKey);
        #endregion


        #region Message
        public Task<ResponseModel> MessageOperationAsync(MessageDbo data);
        public Task<ResponseModel> JobOperationAsync(JobDbo data);
        public Task<RpcResponseModel> RpcMessageTextOperationAsync(Guid transactionId, string consumerKey, string message);
        public Task<RpcResponseModel> RpcMessageFunctionOperationAsync(Guid transactionId, string consumerKey, string message);
        public Task RpcMessageResponseOperationAsync(Guid transactionId, string resultData, bool isError, string errorMessage);
        #endregion


        #region Started Completed
        internal Task<ResponseModel> StartedOperation(string data);
        internal Task<ResponseBaseModel> CompletedOperation(string data);
        #endregion
    }
}
