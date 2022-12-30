using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Broker;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Config;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Broker.Constants
{
    internal class DefaultBrokerConst
    {
        internal const MessageBrokerFactoryEnum BrokerFactory = MessageBrokerFactoryEnum.Default;     
    }
}
