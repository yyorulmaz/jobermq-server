using JoberMQ.Business.Abstraction.DboCreator;
using JoberMQ.Business.Abstraction.DbOprService;
using JoberMQ.Business.Implementation.DboCreator.Default;
using JoberMQ.Entities.Enums.DbOpr;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DboCreatorFactory
    {
        internal static IDboCreator CreateDboCreator(DboCreatorFactoryEnum dboCreatorFactory, IDbOprService dbOprService)
        {
            IDboCreator dboCreator;

            switch (dboCreatorFactory)
            {
                case DboCreatorFactoryEnum.Default:
                    dboCreator = new DfDboCreator(dbOprService);
                    break;
                default:
                    dboCreator = new DfDboCreator(dbOprService);
                    break;
            }

            return dboCreator;
        }
    }
}
