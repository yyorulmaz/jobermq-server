using JoberMQ.Abstraction.Database;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Implementation.Database.Default;

namespace JoberMQ.Factories.Database
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
