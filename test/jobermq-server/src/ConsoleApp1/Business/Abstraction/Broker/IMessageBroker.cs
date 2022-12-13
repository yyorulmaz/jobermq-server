using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Business.Abstraction.Broker
{
    internal interface IMessageBroker
    {
        public bool Start();
    }
}
