using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQ.Abstraction
{
    public interface IJoberMQHost
    {
        internal JoberMQ.Abstraction.Configuration.IConfiguration Configuration { get; }
        public Task StartAsync(IHubContext<JoberHub> hubContext = null);
    }
}
