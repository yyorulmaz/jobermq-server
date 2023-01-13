using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Database;
using JoberMQ.Database.Abstraction.DboCreator;
using JoberMQ.Database.Implementation.DboCreator.Default;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;

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
