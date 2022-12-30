using JoberMQ.Entities.Enums.Broker;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Implementation.Broker.Default;
using JoberMQNEW.Server.Abstraction.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Server.Factories.Broker
{
    internal class BrokerFactory
    {
        internal static IBroker CreateBroker(ServerConfigModel serverConfig, IDbOprService dbOprService, IClientService clientService)
        {
            IBroker broker;

            switch (serverConfig.BrokerConfig.BrokerFactory)
            {
                case BrokerFactoryEnum.Default:
                    broker = new DfBroker(serverConfig, dbOprService, clientService);
                    break;
                default:
                    broker = new DfBroker(serverConfig, dbOprService, clientService);
                    break;
            }

            return broker;
        }
    }
}
