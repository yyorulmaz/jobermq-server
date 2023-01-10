using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationMessage : IConfigurationMessage
    {
        MemFactoryEnum messageMasterFactory = DefaultMessageConst.MessageMasterFactory;
        public MemFactoryEnum MessageMasterFactory { get => messageMasterFactory; set => messageMasterFactory = value; }
        MemDataFactoryEnum messageMasterDataFactory = DefaultMessageConst.MessageMasterDataFactory;
        public MemDataFactoryEnum MessageMasterDataFactory { get => messageMasterDataFactory; set => messageMasterDataFactory = value; }
    }
}
