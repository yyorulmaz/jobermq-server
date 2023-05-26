using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationClient
    {
        public ClientMasterDataFactoryEnum ClientMasterDataFactory { get; set; }
        public ClientChildDataFactoryEnum ClientChildDataFactory { get; set; }

        public ClientFactoryEnum ClientFactory { get; set; }
    }
}
