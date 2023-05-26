﻿using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultClientConst
    {
        internal const ClientMasterDataFactoryEnum ClientMasterDataFactory = ClientMasterDataFactoryEnum.Default;
        internal const ClientChildDataFactoryEnum ClientChildDataFactory = ClientChildDataFactoryEnum.Default;

        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
    }
}
