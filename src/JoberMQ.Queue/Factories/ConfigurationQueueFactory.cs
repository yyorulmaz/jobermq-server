using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Database.Implementation.Configuration.Default;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Implementation.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Queue.Factories
{
    internal class ConfigurationQueueFactory
    {
        internal static IConfigurationQueue Create(ConfigurationQueueFactoryEnum configurationQueueFactory)
        {
            IConfigurationQueue configurationQueue;

            switch (configurationQueueFactory)
            {
                case ConfigurationQueueFactoryEnum.Default:
                    configurationQueue = new DfConfigurationQueue();
                    break;
                default:
                    configurationQueue = new DfConfigurationQueue();
                    break;
            }

            return configurationQueue;
        }
    }
}
