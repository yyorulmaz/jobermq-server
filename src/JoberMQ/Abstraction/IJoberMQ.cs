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
    internal interface IJoberMQ
    {
        internal IConfiguration Configuration { get; }
        internal IStatusCode StatusCode { get; }
        internal IClients Clients { get; }
        internal IHubContext<JoberHub> JoberHubContext { get; }
        internal IMemRepository<Guid, MessageDbo> MessageMasterData { get; }
        internal IDatabase Database { get; }
        internal ISchedule Schedule { get; }
        internal IMessageBroker MessageBroker { get; }
        internal IMemRepository<string, IMessageDistributor> Distributors { get; }
        internal IMemRepository<string, IMessageQueue> Queues { get; }

        Task StartAsync();





        #region Connect
        internal Task<bool> ConnectedOperationAsync(HubCallerContext context);
        internal Task<bool> DisconnectedOperationAsync(HubCallerContext context);
        #endregion


        #region Distributor
        internal Task<ResponseBaseModel<DistributorModel>> DistributorOperationGetAsync(string data);
        internal Task<ResponseBaseModel> DistributorOperationCreateAsync(DistributorModel data);
        internal Task<ResponseBaseModel> DistributorOperationEditAsync(DistributorModel data);
        internal Task<ResponseBaseModel> DistributorOperationRemoveAsync(string data);
        #endregion


        #region Queue
        internal Task<ResponseBaseModel<QueueModel>> QueueOperationGetAsync(string data);
        internal Task<ResponseBaseModel> QueueOperationCreateAsync(QueueModel data);
        internal Task<ResponseBaseModel> QueueOperationEditAsync(QueueModel data);
        internal Task<ResponseBaseModel> QueueOperationRemoveAsync(string data);
        internal Task<ResponseBaseModel> QueueOperationBindAsync(string data);
        #endregion


        #region Consume
        internal Task<ResponseBaseModel> ConsumeOperationSubAsync(string clientKey, string queueKey, bool isDurable);
        internal Task<ResponseBaseModel> ConsumeOperationUnSubAsync(string clientKey, string queueKey);
        #endregion


        #region Message
        internal Task<ResponseModel> MessageOperationAsync(MessageDbo data);
        internal Task<ResponseModel> JobOperationAsync(string data);
        internal Task<RpcResponseModel> RpcOperationAsync(string data);
        internal Task RpcResponseOperationAsync(string rpc);
        #endregion
    }
}
