using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoberMQ.Common.Enums.Broker;
using JoberMQ.Abstraction.Broker;
using JoberMQ.Implementation.Broker.Default;

namespace JoberMQ.Factories.Broker
{
    internal class MessageBrokerFactory
    {
        public static IMessageBroker Create(MessageBrokerFactoryEnum messageBrokerFactory)
        {
            IMessageBroker result;

            switch (messageBrokerFactory)
            {
                case MessageBrokerFactoryEnum.Default:
                    result = new DefaultMessageBroker();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
