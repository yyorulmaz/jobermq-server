using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Hubs;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.Models.Rpc;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Timing.Abstraction;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace JoberMQ.Abstraction.Jober
{
    public interface IJober
    {
        public Task StartAsync();


        //internal bool IsJoberActive { get; set; }
        internal IConfiguration Configuration { get; }
        internal IStatusCode StatusCode { get; }
        internal IDatabase Database { get; }
        internal IClientMasterData ClientMasterData { get; }
        internal IMemRepository<Guid, MessageDbo> MessageMasterData { get; }
        internal IMessageBroker MessageBroker { get; }
        internal ISchedule Schedule { get; }
        internal IHubContext<JoberHub> JoberHubContext { get; }
       


        internal Task<bool> ConnectedOperation(HubCallerContext context);
        internal Task<bool> DisConnectedOperation(HubCallerContext context);


        internal Task<ResponseModel> DistributorOperation(string distributorData);
        internal Task<ResponseModel> QueueOperation(string queueData);
        internal Task<ResponseModel> ConsumeOperation(string connectionId, string consumeData);



        internal Task<ResponseModel> JobOperation(string job);
        internal Task<ResponseModel> MessageOperation(string message);
        internal Task<RpcResponseModel> RpcOperation(string rpc);
        internal Task RpcResponseOperation(string rpc);
        

        internal Task<ResponseModel> MessageStartedOperation(string data);
        internal Task<ResponseModel> MessageCompletedOperation(string data);



        internal ConcurrentDictionary<Guid, Channel<RpcResponseModel>> Channels { get; set; }
    }
}
