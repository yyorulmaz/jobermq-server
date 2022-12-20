using JoberMQ.Abstraction.Database;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Implementation.Database.Default;
using JoberMQ.Server.Factories.DbOpr;

namespace JoberMQ.Factories.Database
{
    internal class DatabaseServiceFactory
    {
        internal static IDatabaseService CreateDatabaseService(IConfigurationDatabase configuration)
        {
            var dbOprs = DbOprFactory.CreateDbOprs(configuration);

            IDatabaseService databaseService;

            switch (configuration.DatabaseServiceFactory)
            {
                case DatabaseServiceFactoryEnum.Default:
                    databaseService = new DfDatabaseManager(
                        dbOprs.UserDbOpr,
                        dbOprs.DistributorDbOpr,
                        dbOprs.QueueDbOpr,
                        dbOprs.EventSubDbOpr,
                        dbOprs.JobDbOpr,
                        dbOprs.JobTransactionDbOpr,
                        dbOprs.MessageDbOpr,
                        dbOprs.MessageResultDbOpr);
                    break;
                default:
                    databaseService = new DfDatabaseManager(
                        dbOprs.UserDbOpr,
                        dbOprs.DistributorDbOpr,
                        dbOprs.QueueDbOpr,
                        dbOprs.EventSubDbOpr,
                        dbOprs.JobDbOpr,
                        dbOprs.JobTransactionDbOpr,
                        dbOprs.MessageDbOpr,
                        dbOprs.MessageResultDbOpr);
                    break;
            }

            return databaseService;
        }
    }
}
