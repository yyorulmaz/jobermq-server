using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Implementation.DbOpr.Default;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DbOprServiceFactory
    {
        internal static IDbOprService CreateDbOprService(
            DbOprServiceFactoryEnum dbOprServiceFactory, 
            DbOprFactoryEnum dbOprFactory, 
            DbMemFactoryEnum dbMemFactory, 
            DbMemDataFactoryEnum dbMemDataFactory, 
            DbTextFactoryEnum dbTextFactory)
        {
            var dbOprs = DbOprFactory.CreateDbOprs(dbOprFactory, dbMemFactory, dbMemDataFactory, dbTextFactory);

            IDbOprService dbOprService;

            switch (dbOprServiceFactory)
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
