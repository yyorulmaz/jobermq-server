using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Implementation.Default;
using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Enums.Consume;

namespace JoberMQ.Client.Factories
{
    public class ClientChildDataFactory
    {
        //public static IClientChildData Create(ClientChildDataFactoryEnum clientChildDataFactory, IClientMasterData clientMasterData, ConsumeTypeEnum consumeType, string consumeKey)
        public static IClientChildData Create(ClientChildDataFactoryEnum clientChildDataFactory, IClientMasterData clientMasterData, string consumeKey)
        {
            IClientChildData clientChildData;

            switch (clientChildDataFactory)
            {
                case ClientChildDataFactoryEnum.Default:
                    //clientChildData = new DfClientChildData(clientMasterData, consumeType, consumeKey);
                    clientChildData = new DfClientChildData(clientMasterData, consumeKey);
                    break;
                default:
                    //clientChildData = new DfClientChildData(clientMasterData, consumeType, consumeKey);
                    clientChildData = new DfClientChildData(clientMasterData, consumeKey);
                    break;
            }

            return clientChildData;
        }
    }
}
