using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.DataAccess.Implementation.DbMem.Default;
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
                            userMemDal = new DfUserMemDal(JoberMQ.Data.MemoryData.UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(JoberMQ.Data.MemoryData.DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(JoberMQ.Data.MemoryData.QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(JoberMQ.Data.MemoryData.EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JoberMQ.Data.MemoryData.JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JoberMQ.Data.MemoryData.JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(JoberMQ.Data.MemoryData.MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(JoberMQ.Data.MemoryData.MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(JoberMQ.Data.MemoryData.UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(JoberMQ.Data.MemoryData.DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(JoberMQ.Data.MemoryData.QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(JoberMQ.Data.MemoryData.EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JoberMQ.Data.MemoryData.JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JoberMQ.Data.MemoryData.JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(JoberMQ.Data.MemoryData.MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(JoberMQ.Data.MemoryData.MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(JoberMQ.Data.MemoryData.UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(JoberMQ.Data.MemoryData.DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(JoberMQ.Data.MemoryData.QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(JoberMQ.Data.MemoryData.EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JoberMQ.Data.MemoryData.JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JoberMQ.Data.MemoryData.JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(JoberMQ.Data.MemoryData.MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(JoberMQ.Data.MemoryData.MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(JoberMQ.Data.MemoryData.UserMemData.UserDatas);
                            distributorMemDal = new DfDistributorMemDal(JoberMQ.Data.MemoryData.DistributorMemData.DistributorDatas);
                            queueMemDal = new DfQueueMemDal(JoberMQ.Data.MemoryData.QueueMemData.QueueDatas);
                            eventSubMemDal = new DfEventSubMemDal(JoberMQ.Data.MemoryData.EventSubMemData.EventSubDatas);
                            jobMemDal = new DfJobMemDal(JoberMQ.Data.MemoryData.JobMemData.JobDatas);
                            jobTransactionMemDal = new DfJobTransactionMemDal(JoberMQ.Data.MemoryData.JobTransactionMemData.JobTransactionDatas);
                            messageMemDal = new DfMessageMemDal(JoberMQ.Data.MemoryData.MessageMemData.MessageDatas);
                            messageResultMemDal = new DfMessageResultMemDal(JoberMQ.Data.MemoryData.MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
            }



            return (userMemDal, distributorMemDal, queueMemDal, eventSubMemDal, jobMemDal, jobTransactionMemDal, messageMemDal, messageResultMemDal);
        }
    }
}
