using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.DbMem.Default;
using JoberMQ.Database.MemoryData;
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
            IJobMemDal JobMemDal,
            IJobTransactionMemDal JobTransactionMemDal,
            IMessageMemDal MessageMemDal,
            IMessageResultMemDal MessageResultMemDal)
            CreateDbMems(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IUserMemDal userMemDal;
            IDistributorMemDal distributorMemDal;
            IQueueMemDal queueMemDal;
            IEventSubMemDal eventSubMemDal;
            IJobMemDal jobMemDal;
            IJobTransactionMemDal jobTransactionMemDal;
            IMessageMemDal messageMemDal;
            IMessageResultMemDal messageResultMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
            }



            return (userMemDal, distributorMemDal, queueMemDal, eventSubMemDal, jobMemDal, jobTransactionMemDal, messageMemDal, messageResultMemDal);
        }
    }
}
