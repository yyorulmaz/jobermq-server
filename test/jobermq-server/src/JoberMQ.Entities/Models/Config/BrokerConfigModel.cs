using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Broker;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class BrokerConfigModel
    {
        internal BrokerFactoryEnum BrokerFactory => ServerConst.Broker.BrokerFactory;
        internal QueueFactoryEnum QueueFactory => ServerConst.Broker.QueueFactory;
        internal QueueChildPriorityFactoryEnum QueueChildPriorityFactory => ServerConst.Broker.QueueChildPriorityFactory;
        internal QueueChildFIFOFactoryEnum QueueChildFIFOFactory => ServerConst.Broker.QueueChildFIFOFactory;
        internal QueueChildLIFOFactoryEnum QueueChildLIFOFactory => ServerConst.Broker.QueueChildLIFOFactory;
        internal DistributorFactoryEnum DistributorFactory => ServerConst.Broker.DistributorFactory;

        internal ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas = ServerConst.Broker.DefaultDistributorConfigDatas;
        internal ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigDatas = ServerConst.Broker.DefaultQueueConfigDatas;

    }
}
