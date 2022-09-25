using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Publisher;
using JoberMQ.Server.Abstraction.Timing;
using JoberMQ.Server.Hubs;
using JoberMQNEW.Server.Abstraction.Client;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQ.Server.Abstraction.Server
{
    public interface IServer
    {
        internal bool IsServerActive { get; set; }

        public ServerConfigModel ServerConfig { get; }
        internal IStatusCode StatusCode { get; }
        internal IDbOprService DbOprService { get; }
        internal IDboCreator DboCreator { get; }
        internal IClientService ClientService { get; }
        internal ISchedule Schedule { get; }
        internal IBroker Broker { get; }
        

        internal IHubContext<JoberHub> JoberHubContext { get; }


        public void Start();
    }
}
