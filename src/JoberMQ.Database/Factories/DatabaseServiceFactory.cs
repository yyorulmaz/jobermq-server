using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Database.Implementation.DbService.Default;
using JoberMQ.Common.Enums.Database;

namespace JoberMQ.Database.Factories
{
    internal class DatabaseServiceFactory
    {
        internal static IDatabaseService CreateDatabaseService(IConfigurationDatabase configuration)
        {
            IDatabaseService databaseService;

            switch (configuration.DatabaseServiceFactory)
            {
                case DatabaseServiceFactoryEnum.Default:
                    databaseService = new DfDatabaseManager(configuration);
                    break;
                default:
                    databaseService = new DfDatabaseManager(configuration);
                    break;
            }

            return databaseService;
        }
    }
}
