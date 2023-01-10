using JoberMQ.Common.Enums.Database;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Database.Implementation.DbService.Default;

namespace JoberMQ.Database.Factories
{
    internal class DatabaseFactory
    {
        internal static IDatabase Create(IConfigurationDatabase configuration)
        {
            IDatabase databaseService;

            switch (configuration.DatabaseServiceFactory)
            {
                case DatabaseServiceFactoryEnum.Default:
                    databaseService = new DfDatabase(configuration);
                    break;
                default:
                    databaseService = new DfDatabase(configuration);
                    break;
            }

            return databaseService;
        }
    }
}
