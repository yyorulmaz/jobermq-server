using JoberMQ.Common.Database.Enums;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationMessage : IConfigurationMessage
    {
        MemFactoryEnum messageMasterFactory = ConfigurationMessageConst.MessageMasterFactory;
        public MemFactoryEnum MessageMasterFactory { get => messageMasterFactory; set => messageMasterFactory = value; }
        MemDataFactoryEnum messageMasterDataFactory = ConfigurationMessageConst.MessageMasterDataFactory;
        public MemDataFactoryEnum MessageMasterDataFactory { get => messageMasterDataFactory; set => messageMasterDataFactory = value; }
    }
}
