using JoberMQ.Business.Abstraction.Host;
using JoberMQ.Business.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ
{
    internal static class JoberHost
    {
        public static IJoberHostBuilder DefaultConfiguration()
        {
            var sss = JoberMQ.Business.Factories.Configuration.ConfigurationFactory.CreateConfiguration(DefaultConfigurationConst.ConfigurationFactory);
            return null;
        }
    }

    

}
