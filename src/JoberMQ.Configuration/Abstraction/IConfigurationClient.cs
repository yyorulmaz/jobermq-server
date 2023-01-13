using JoberMQ.Common.Enums.Client;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationClient
    {
        public MemFactoryEnum ClientMasterFactory { get; set; }
        public MemDataFactoryEnum ClientMasterDataFactory { get; set; }
        
        public ClientFactoryEnum ClientFactory { get; set; }
    }
}