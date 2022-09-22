using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Implementation.DbOpr.Default;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DbOprServiceFactory
    {
        internal static IDbOprService CreateDbOprService(DbOprConfigModel dbOprConfig)
        {
            var dbOprs = DbOprFactory.CreateDbOprs(dbOprConfig);

            IDbOprService dbOprService;

            switch (dbOprConfig.DbOprServiceFactory)
            {
                case DbOprServiceFactoryEnum.Default:
                    dbOprService = new DfDbOprManager(
                dbOprs.UserDbOpr,
                dbOprs.DistributorDbOpr,
                dbOprs.QueueDbOpr,
                dbOprs.EventSubDbOpr,
                dbOprs.JobDataDbOpr,
                dbOprs.JobDbOpr,
                dbOprs.MessageDbOpr,
                dbOprs.MessageResultDbOpr);
                    break;
                default:
                    dbOprService = new DfDbOprManager(
                dbOprs.UserDbOpr,
                dbOprs.DistributorDbOpr,
                dbOprs.QueueDbOpr,
                dbOprs.EventSubDbOpr,
                dbOprs.JobDataDbOpr,
                dbOprs.JobDbOpr,
                dbOprs.MessageDbOpr,
                dbOprs.MessageResultDbOpr);
                    break;
            }

            return dbOprService;
        }
    }
}
