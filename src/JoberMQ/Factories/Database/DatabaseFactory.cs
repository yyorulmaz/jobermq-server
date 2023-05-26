using JoberMQ.Common.Enums.Database;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Database;
using JoberMQ.Implementation.Database.Default;

namespace JoberMQ.Factories.Database
{
    internal class DatabaseFactory
    {
        internal static IDatabase Create(IConfigurationDatabase configuration)
        {
            IDatabase databaseService;

            switch (configuration.DatabaseServiceFactory)
            {
                case DatabaseServiceFactoryEnum.Default:
                    databaseService = new DefaultDatabase(configuration);
                    break;
                default:
                    throw new System.Exception("none");
            }

            return databaseService;
        }
    }
}
