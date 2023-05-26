using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoberMQ.Abstraction.Configuration;

namespace JoberMQ.Abstraction
{
    public interface IJoberMQHost
    {
        internal IConfiguration Configuration { get; }
        public Task StartAsync();
    }
}
