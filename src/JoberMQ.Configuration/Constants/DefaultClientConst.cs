using JoberMQ.Common.Enums.Client;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultClientConst
    {
        internal const MemFactoryEnum ClientMasterFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum ClientMasterDataFactory = MemDataFactoryEnum.Data;
        
        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
    }
}
