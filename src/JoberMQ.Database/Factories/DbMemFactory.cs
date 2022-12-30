using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Database.Implementation.DbMem.Default;
using JoberMQ.Database.Data;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Factories
{
    internal class DbMemFactory
    {
        internal static (
            IUserDbMem UserMem,
            IDistributorDbMem DistributorMem,
            IQueueDbMem QueueMem,
            IEventSubDbMem EventSubMem,
            IJobDbMem JobMem,
            IJobTransactionDbMem JobTransactionMem,
            IMessageDbMem MessageMem,
            IMessageResultDbMem MessageResultMem)
            CreateDbMems(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IUserDbMem userMem;
            IDistributorDbMem distributorMem;
            IQueueDbMem queueMem;
            IEventSubDbMem eventSubMem;
            IJobDbMem jobMem;
            IJobTransactionDbMem jobTransactionMem;
            IMessageDbMem messageMem;
            IMessageResultDbMem messageResultMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
            }



            return (userMem, distributorMem, queueMem, eventSubMem, jobMem, jobTransactionMem, messageMem, messageResultMem);
        }

        internal static IUserDbMem CreateDbMemUser(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IUserDbMem userMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            break;
                        default:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            break;
                        default:
                            userMem = new DfUserMem(UserMemData.UserDatas);
                            break;
                    }
                    break;
            }

            return userMem;
        }
        internal static IDistributorDbMem CreateDbMemDistributor(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IDistributorDbMem distributorMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            break;
                        default:
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            break;
                        default:
                            distributorMem = new DfDistributorMem(DistributorMemData.DistributorDatas);
                            break;
                    }
                    break;
            }

            return distributorMem;
        }
        internal static IQueueDbMem CreateDbMemQueue(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IQueueDbMem queueMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            break;
                        default:
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            break;
                        default:
                            queueMem = new DfQueueMem(QueueMemData.QueueDatas);
                            break;
                    }
                    break;
            }

            return queueMem;
        }
        internal static IEventSubDbMem CreateDbMemEventSub(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IEventSubDbMem eventSubMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            break;
                        default:
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            break;
                        default:
                            eventSubMem = new DfEventSubMem(EventSubMemData.EventSubDatas);
                            break;
                    }
                    break;
            }

            return eventSubMem;
        }
        internal static IJobDbMem CreateDbMemJob(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IJobDbMem jobMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            break;
                        default:
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            break;
                        default:
                            jobMem = new DfJobMem(JobMemData.JobDatas);
                            break;
                    }
                    break;
            }

            return jobMem;
        }
        internal static IJobTransactionDbMem CreateDbMemJobTransaction(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IJobTransactionDbMem jobTransactionMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            break;
                        default:
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            break;
                        default:
                            jobTransactionMem = new DfJobTransactionMem(JobTransactionMemData.JobTransactionDatas);
                            break;
                    }
                    break;
            }

            return jobTransactionMem;
        }
        internal static IMessageDbMem CreateDbMemMessage(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IMessageDbMem messageMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            break;
                        default:
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            break;
                        default:
                            messageMem = new DfMessageMem(MessageMemData.MessageDatas);
                            break;
                    }
                    break;
            }

            return messageMem;
        }
        internal static IMessageResultDbMem CreateDbMemMessageResult(MemFactoryEnum dbMemFactory, MemDataFactoryEnum dbMemDataFactory)
        {
            IMessageResultDbMem messageResultMem;

            switch (dbMemFactory)
            {
                case MemFactoryEnum.Default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
                default:
                    switch (dbMemDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                        default:
                            messageResultMem = new DfMessageResultMem(MessageResultMemData.MessageResultDatas);
                            break;
                    }
                    break;
            }

            return messageResultMem;
        }

        internal static IMemRepository<TKey, TValue> CreateDefault<TKey, TValue>(ConcurrentDictionary<TKey, TValue> MasterData)
            => new DfMemRepository<TKey, TValue>(MasterData);
    }
}
