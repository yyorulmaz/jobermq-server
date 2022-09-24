using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Implementation.DboCreator.Default;

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
