using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Hubs;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.StatusCode.Abstraction;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Abstraction.Jober
{
    public interface IJober
    {
        // NodeKey yapısını load balancer da yapıcam
        //internal string JoberKey { get; }
        //internal string NodeKey { get; }
        internal bool IsJoberActive { get; set; }
        internal IConfiguration Configuration { get; }
        internal IStatusCode StatusCode { get; }
        internal IMemRepository<string, IClient> ClientMaster { get; }
        internal IMemRepository<Guid, MessageDbo> MessageMaster { get; }
        internal IDatabase Database { get; }
        internal IMessageBroker MessageBroker { get; }


        //internal ISchedule Schedule { get; }



        internal IHubContext<JoberHub> JoberHubContext { get; }

        Task StartAsync();
    }
}
