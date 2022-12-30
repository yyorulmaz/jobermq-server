using JoberMQ.Common.Enums.Database;
using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Database.Implementation.DBOCreator.Default;

namespace JoberMQ.Database.Factories
{
    internal class DboCreatorFactory
    {
        internal static IDboCreator CreateDboCreator(DboCreatorFactoryEnum dboCreatorFactory, IJobDbOpr jobDbOpr)
        {
            IDboCreator dboCreator;

            switch (dboCreatorFactory)
            {
                case DboCreatorFactoryEnum.Default:
                    dboCreator = new DfDboCreator(jobDbOpr);
                    break;
                default:
                    dboCreator = new DfDboCreator(jobDbOpr);
                    break;
            }

            return dboCreator;
        }
    }
}
