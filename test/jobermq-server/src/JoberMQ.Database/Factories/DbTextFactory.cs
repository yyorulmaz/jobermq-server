using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbText;
using JoberMQ.Database.Implementation.DbText.Default;
using JoberMQ.Entities.Enums.DbOpr;
using System.Linq;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DbTextFactory
    {
        internal static (
            IUserTextDal UserTextDal,
            IDistributorTextDal DistributorTextDal,
            IQueueTextDal QueueTextDal,
            IEventSubTextDal EventSubTextDal,
            IJobTextDal JobTextDal,
            IJobTransactionTextDal JobTransactionTextDal,
            IMessageTextDal MessageTextDal,
            IMessageResultTextDal MessageResultTextDal)
            CreateDbTexts(IConfigurationDatabase config)
        {
            IUserTextDal userTextDal;
            IDistributorTextDal distributorTextDal;
            IQueueTextDal queueTextDal;
            IEventSubTextDal eventSubTextDal;
            IJobTextDal jobTextDal;
            IJobTransactionTextDal jobTransactionTextDal;
            IMessageTextDal messageTextDal;
            IMessageResultTextDal messageResultTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    userTextDal = new DfUserTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x=>x.Key == "User").Value);
                    distributorTextDal = new DfDistributorTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueTextDal = new DfQueueTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubTextDal = new DfEventSubTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobTextDal = new DfJobTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    jobTransactionTextDal = new DfJobTransactionTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    messageTextDal = new DfMessageTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultTextDal = new DfMessageResultTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
                default:
                    userTextDal = new DfUserTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    distributorTextDal = new DfDistributorTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueTextDal = new DfQueueTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubTextDal = new DfEventSubTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobTextDal = new DfJobTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    jobTransactionTextDal = new DfJobTransactionTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    messageTextDal = new DfMessageTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultTextDal = new DfMessageResultTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
            }

            return (userTextDal, distributorTextDal, queueTextDal, eventSubTextDal, jobTextDal, jobTransactionTextDal, messageTextDal, messageResultTextDal);
        }

        internal static IUserTextDal CreateDbTextUser(IConfigurationDatabase config)
        {
            IUserTextDal userTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    userTextDal = new DfUserTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    break;
                default:
                    userTextDal = new DfUserTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    break;
            }

            return userTextDal;
        }
        internal static IDistributorTextDal CreateDbTextDistributor(IConfigurationDatabase config)
        {
            IDistributorTextDal distributorTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    distributorTextDal = new DfDistributorTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    break;
                default:
                    distributorTextDal = new DfDistributorTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    break;
            }

            return distributorTextDal;
        }
        internal static IQueueTextDal CreateDbTextQueue(IConfigurationDatabase config)
        {
            IQueueTextDal queueTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    queueTextDal = new DfQueueTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    break;
                default:
                    queueTextDal = new DfQueueTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    break;
            }

            return queueTextDal;
        }
        internal static IEventSubTextDal CreateDbTextEventSub(IConfigurationDatabase config)
        {
            IEventSubTextDal eventSubTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    eventSubTextDal = new DfEventSubTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    break;
                default:
                    eventSubTextDal = new DfEventSubTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    break;
            }

            return eventSubTextDal;
        }
        internal static IJobTextDal CreateDbTextJob(IConfigurationDatabase config)
        {
            IJobTextDal jobTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    jobTextDal = new DfJobTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    break;
                default:
                    jobTextDal = new DfJobTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    break;
            }

            return jobTextDal;
        }
        internal static IJobTransactionTextDal CreateDbTextJobTransaction(IConfigurationDatabase config)
        {
            IJobTransactionTextDal jobTransactionTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    jobTransactionTextDal = new DfJobTransactionTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    break;
                default:
                    jobTransactionTextDal = new DfJobTransactionTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    break;
            }

            return jobTransactionTextDal;
        }
        internal static IMessageTextDal CreateDbTextMessage(IConfigurationDatabase config)
        {
            IMessageTextDal messageTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    messageTextDal = new DfMessageTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    break;
                default:
                    messageTextDal = new DfMessageTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    break;
            }

            return messageTextDal;
        }
        internal static IMessageResultTextDal CreateDbTextMessageResult(IConfigurationDatabase config)
        {
            IMessageResultTextDal messageResultTextDal;

            switch (config.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    messageResultTextDal = new DfMessageResultTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
                default:
                    messageResultTextDal = new DfMessageResultTextDal(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
            }

            return messageResultTextDal;
        }
    }
}
