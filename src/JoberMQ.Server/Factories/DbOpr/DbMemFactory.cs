using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.DataAccess.Implementation.DbMem.Default;
using JoberMQ.DataAccess.InMemory;
using JoberMQ.Entities.Enums.DbOpr;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DbMemFactory
    {
        internal static (
            IUserMemDal UserMemDal,
            IDistributorMemDal DistributorMemDal,
            IQueueMemDal QueueMemDal,
            IEventSubMemDal EventSubMemDal,
            IJobDataMemDal JobDataMemDal,
            IJobMemDal JobMemDal,
            IMessageMemDal MessageMemDal,
            IMessageResultMemDal MessageResultMemDal)
            CreateDbMems(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IUserMemDal userMemDal;
            IDistributorMemDal distributorMemDal;
            IQueueMemDal queueMemDal;
            IEventSubMemDal eventSubMemDal;
            IJobDataMemDal jobDataMemDal;
            IJobMemDal jobMemDal;
            IMessageMemDal messageMemDal;
            IMessageResultMemDal messageResultMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.None:
                            userMemDal = new DfUserMemDal();
                            distributorMemDal = new DfDistributorMemDal();
                            queueMemDal = new DfQueueMemDal();
                            eventSubMemDal = new DfEventSubMemDal();
                            jobDataMemDal = new DfJobDataMemDal();
                            jobMemDal = new DfJobMemDal();
                            messageMemDal = new DfMessageMemDal();
                            messageResultMemDal = new DfMessageResultMemDal();
                            break;
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(DboInMemory.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DboInMemory.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(DboInMemory.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(DboInMemory.EventSubDatas);
                            jobDataMemDal = new DfJobDataMemDal(DboInMemory.JobDataDatas);
                            jobMemDal = new DfJobMemDal(DboInMemory.JobDatas);
                            messageMemDal = new DfMessageMemDal(DboInMemory.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(DboInMemory.MessageResultDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(DboInMemory.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DboInMemory.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(DboInMemory.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(DboInMemory.EventSubDatas);
                            jobDataMemDal = new DfJobDataMemDal(DboInMemory.JobDataDatas);
                            jobMemDal = new DfJobMemDal(DboInMemory.JobDatas);
                            messageMemDal = new DfMessageMemDal(DboInMemory.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(DboInMemory.MessageResultDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.None:
                            userMemDal = new DfUserMemDal();
                            distributorMemDal = new DfDistributorMemDal();
                            queueMemDal = new DfQueueMemDal();
                            eventSubMemDal = new DfEventSubMemDal();
                            jobDataMemDal = new DfJobDataMemDal();
                            jobMemDal = new DfJobMemDal();
                            messageMemDal = new DfMessageMemDal();
                            messageResultMemDal = new DfMessageResultMemDal();
                            break;
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(DboInMemory.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DboInMemory.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(DboInMemory.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(DboInMemory.EventSubDatas);
                            jobDataMemDal = new DfJobDataMemDal(DboInMemory.JobDataDatas);
                            jobMemDal = new DfJobMemDal(DboInMemory.JobDatas);
                            messageMemDal = new DfMessageMemDal(DboInMemory.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(DboInMemory.MessageResultDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(DboInMemory.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DboInMemory.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(DboInMemory.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(DboInMemory.EventSubDatas);
                            jobDataMemDal = new DfJobDataMemDal(DboInMemory.JobDataDatas);
                            jobMemDal = new DfJobMemDal(DboInMemory.JobDatas);
                            messageMemDal = new DfMessageMemDal(DboInMemory.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(DboInMemory.MessageResultDatas);
                            break;
                    }
                    break;
            }



            return (userMemDal, distributorMemDal, queueMemDal, eventSubMemDal, jobDataMemDal, jobMemDal, messageMemDal, messageResultMemDal);
        }
    }
}
