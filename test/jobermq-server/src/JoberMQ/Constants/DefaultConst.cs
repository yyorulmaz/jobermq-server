using JoberMQ.Entities.Enums.Configuration;
using JoberMQ.Entities.Enums.Host;
using JoberMQ.Entities.Enums.Jober;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Constants
{
    internal class DefaultConst
    {
        internal const HostFactoryEnum HostFactory = HostFactoryEnum.Default;
        internal const ConfigurationFactoryEnum ConfigurationFactory = ConfigurationFactoryEnum.Default;
        internal const JoberFactoryEnum JoberFactory = JoberFactoryEnum.Default;
        
    }
}
