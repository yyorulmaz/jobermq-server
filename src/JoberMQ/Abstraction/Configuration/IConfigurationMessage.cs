using JoberMQ.Common.Database.Enums;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationMessage
    {
        public MemFactoryEnum MessageMasterFactory { get; set; }
        public MemDataFactoryEnum MessageMasterDataFactory { get; set; }
    }
}
