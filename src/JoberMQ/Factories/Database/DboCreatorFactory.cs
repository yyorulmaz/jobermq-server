using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Database;
using JoberMQ.Abstraction.Database;
using JoberMQ.Implementation.Database.Default;

namespace JoberMQ.Factories.Database
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
                    throw new System.Exception("none");
            }

            return dboCreator;
        }
    }
}
