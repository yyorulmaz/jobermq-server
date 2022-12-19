using JoberMQ.Business.Abstraction.DbOprService;
using JoberMQ.Business.Implementation.DbOprService.Default;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;

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
                    dbOprService = new DfDbOprService(
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
                    dbOprService = new DfDbOprService(
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

            return dbOprService;
        }
    }
}
