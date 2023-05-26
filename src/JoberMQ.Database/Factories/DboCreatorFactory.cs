using JoberMQ.Database.Abstraction;
using JoberMQ.Database.Implementation.Default;
using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Database;

namespace JoberMQ.Database.Factories
{
    internal class DboCreatorFactory
    {
        internal static IDboCreator CreateDboCreator(DboCreatorFactoryEnum dboCreatorFactory, IOprRepositoryGuid<JobDbo> jobDbOpr)
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
