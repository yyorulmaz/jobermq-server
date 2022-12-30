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

        internal static IUserMemDal CreateDbMemUser(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IUserMemDal userMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            break;
                        default:
                            userMemDal = new DfUserMemDal(UserMemData.UserDatas);
                            break;
                    }
                    break;
            }

            return userMemDal;
        }
        internal static IDistributorMemDal CreateDbMemDistributor(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IDistributorMemDal distributorMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            break;
                        default:
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            break;
                        default:
                            distributorMemDal = new DfDistributorMemDal(DistributorMemData.DistributorDatas);
                            break;
                    }
                    break;
            }

            return distributorMemDal;
        }
        internal static IQueueMemDal CreateDbMemQueue(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IQueueMemDal queueMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            break;
                        default:
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            break;
                        default:
                            queueMemDal = new DfQueueMemDal(QueueMemData.QueueDatas);
                            break;
                    }
                    break;
            }

            return queueMemDal;
        }
        internal static IEventSubMemDal CreateDbMemEventSub(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IEventSubMemDal eventSubMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            break;
                        default:
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            break;
                        default:
                            eventSubMemDal = new DfEventSubMemDal(EventSubMemData.EventSubDatas);
                            break;
                    }
                    break;
            }

            return eventSubMemDal;
        }
        internal static IJobMemDal CreateDbMemJob(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IJobMemDal jobMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            break;
                        default:
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            break;
                        default:
                            jobMemDal = new DfJobMemDal(JobMemData.JobDatas);
                            break;
                    }
                    break;
            }

            return jobMemDal;
        }
        internal static IJobTransactionMemDal CreateDbMemJobTransaction(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IJobTransactionMemDal jobTransactionMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            break;
                        default:
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            break;
                        default:
                            jobTransactionMemDal = new DfJobTransactionMemDal(JobTransactionMemData.JobTransactionDatas);
                            break;
                    }
                    break;
            }

            return jobTransactionMemDal;
        }
        internal static IMessageMemDal CreateDbMemMessage(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IMessageMemDal messageMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            break;
                        default:
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            break;
                        default:
                            messageMemDal = new DfMessageMemDal(MessageMemData.MessageDatas);
                            break;
                    }
                    break;
            }

            return messageMemDal;
        }
        internal static IMessageResultMemDal CreateDbMemMessageResult(DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory)
        {
            IMessageResultMemDal messageResultMemDal;

            switch (dbMemFactory)
            {
                case DbMemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case DbMemDataFactoryEnum.Data:
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            messageResultMemDal = new DfMessageResultMemDal(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
            }

            return messageResultMemDal;
        }
    }
}
