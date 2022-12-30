using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Abstraction.Jober
{
    public interface IJober
    {
        public IConfiguration Configuration { get; set; }


        internal IDatabaseService DatabaseService { get; set; }




        Task StartAsync();
    }
}
