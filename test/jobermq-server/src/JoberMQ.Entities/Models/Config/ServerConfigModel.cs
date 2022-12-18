using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Enums.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class ServerConfigModel
    {
        internal ServerFactoryEnum ServerFactory => ServerConst.ServerFactory;
        internal ClientFactoryEnum ClientFactory => ServerConst.ClientFactory;
        internal ClientGroupFactoryEnum ClientGroupFactory => ServerConst.ClientGroupFactory;
        internal ClientServiceFactoryEnum ClientServiceFactory => ServerConst.ClientServiceFactory;

        public StatusCodeConfigModel StatusCodeConfig => new StatusCodeConfigModel();
        public SecurityConfigModel SecurityConfig => new SecurityConfigModel();
        public DbOprConfigModel DbOprConfig => new DbOprConfigModel();
        internal BrokerConfigModel BrokerConfig => new BrokerConfigModel();
        public HostConfigModel HostConfig => new HostConfigModel();
        public TimingConfigModel TimingConfig => new TimingConfigModel();
        public PublisherConfigModel PublisherConfig => new PublisherConfigModel();
    }
}
