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

        Task StartAsync(bool owinHost, IHubContext<JoberHub> hubContext = null);





        #region Connect
        public Task<bool> ConnectedOperationAsync(HubCallerContext context);
        public Task<bool> DisconnectedOperationAsync(HubCallerContext context);
        #endregion


        #region Distributor
        public Task<ResponseBaseModel<DistributorModel>> DistributorOperationGetAsync(string data);
        public Task<ResponseBaseModel> DistributorOperationCreateAsync(DistributorModel data);
        public Task<ResponseBaseModel> DistributorOperationEditAsync(DistributorModel data);
        public Task<ResponseBaseModel> DistributorOperationRemoveAsync(string data);
        #endregion


        #region Queue
        public Task<ResponseBaseModel<QueueModel>> QueueOperationGetAsync(string data);
        public Task<ResponseBaseModel> QueueOperationCreateAsync(QueueModel data);
        public Task<ResponseBaseModel> QueueOperationEditAsync(QueueModel data);
        public Task<ResponseBaseModel> QueueOperationRemoveAsync(string data);
        public Task<ResponseBaseModel> QueueOperationBindAsync(string data);
        #endregion


        #region Consume
        public Task<ResponseBaseModel> ConsumeOperationSubAsync(string clientKey, string queueKey, bool isDurable);
        public Task<ResponseBaseModel> ConsumeOperationUnSubAsync(string clientKey, string queueKey);
        #endregion


        #region Message
        public Task<ResponseModel> MessageOperationAsync(MessageDbo data);
        public Task<ResponseModel> JobOperationAsync(string data);
        public Task<RpcResponseModel> RpcOperationAsync(string data);
        public Task RpcResponseOperationAsync(string rpc);
        #endregion
    }
}
