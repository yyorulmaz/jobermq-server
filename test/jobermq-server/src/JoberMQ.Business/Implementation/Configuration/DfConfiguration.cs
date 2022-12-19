using JoberMQ.Business.Abstraction.Configuration;
using System;

namespace JoberMQ.Business.Implementation.Configuration
{
    internal class DfConfiguration : IConfiguration
    {
        IConfigurationData configurationData;
        public IConfigurationData ConfigurationData { get => configurationData; set => configurationData = value; }
    }
}
