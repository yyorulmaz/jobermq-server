using JoberMQ.Abstraction.Configuration;
using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.StatusCode.Abstraction;
using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Hubs;
using JoberMQ.Timing.Abstraction;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace JoberMQ.Abstraction.Jober
{
    public interface IJober
    {
        internal bool IsServerActive { get; set; }
        public IConfiguration Configuration { get; }


        internal IStatusCode StatusCode { get; }
        internal IDatabaseService DatabaseService { get; }
        internal ISchedule Schedule { get; }
        internal IClientService ClientService { get; }
        internal IMessageBroker MessageBroker { get; }



        internal IHubContext<JoberHub> JoberHubContext { get; }

        Task StartAsync();
    }
}
