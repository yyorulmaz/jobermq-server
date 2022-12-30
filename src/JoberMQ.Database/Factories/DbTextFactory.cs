using JoberMQ.Common.Database.Enums;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbText;
using JoberMQ.Database.Implementation.DbText.Default;
using System.Linq;

namespace JoberMQ.Database.Factories
{
    internal class DbTextFactory
    {
        internal static (
            IUserDbText UserText,
            IDistributorDbText DistributorText,
            IQueueDbText QueueText,
            IEventSubDbText EventSubText,
            IJobDbText JobText,
            IJobTransactionDbText JobTransactionText,
            IMessageDbText MessageText,
            IMessageResultDbText MessageResultText)
            CreateDbTexts(IConfigurationDatabase config)
        {
            IUserDbText userText;
            IDistributorDbText distributorText;
            IQueueDbText queueText;
            IEventSubDbText eventSubText;
            IJobDbText jobText;
            IJobTransactionDbText jobTransactionText;
            IMessageDbText messageText;
            IMessageResultDbText messageResultText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    userText = new DfUserText(config.DbTextFileConfigDatas.FirstOrDefault(x=>x.Key == "User").Value);
                    distributorText = new DfDistributorText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueText = new DfQueueText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubText = new DfEventSubText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobText = new DfJobText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    jobTransactionText = new DfJobTransactionText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    messageText = new DfMessageText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultText = new DfMessageResultText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
                default:
                    userText = new DfUserText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    distributorText = new DfDistributorText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueText = new DfQueueText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubText = new DfEventSubText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobText = new DfJobText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    jobTransactionText = new DfJobTransactionText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    messageText = new DfMessageText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultText = new DfMessageResultText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
            }

            return (userText, distributorText, queueText, eventSubText, jobText, jobTransactionText, messageText, messageResultText);
        }

        internal static IUserDbText CreateDbTextUser(IConfigurationDatabase config)
        {
            IUserDbText userText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    userText = new DfUserText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    break;
                default:
                    userText = new DfUserText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    break;
            }

            return userText;
        }
        internal static IDistributorDbText CreateDbTextDistributor(IConfigurationDatabase config)
        {
            IDistributorDbText distributorText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    distributorText = new DfDistributorText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    break;
                default:
                    distributorText = new DfDistributorText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    break;
            }

            return distributorText;
        }
        internal static IQueueDbText CreateDbTextQueue(IConfigurationDatabase config)
        {
            IQueueDbText queueText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    queueText = new DfQueueText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    break;
                default:
                    queueText = new DfQueueText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    break;
            }

            return queueText;
        }
        internal static IEventSubDbText CreateDbTextEventSub(IConfigurationDatabase config)
        {
            IEventSubDbText eventSubText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    eventSubText = new DfEventSubText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    break;
                default:
                    eventSubText = new DfEventSubText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    break;
            }

            return eventSubText;
        }
        internal static IJobDbText CreateDbTextJob(IConfigurationDatabase config)
        {
            IJobDbText jobText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    jobText = new DfJobText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    break;
                default:
                    jobText = new DfJobText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    break;
            }

            return jobText;
        }
        internal static IJobTransactionDbText CreateDbTextJobTransaction(IConfigurationDatabase config)
        {
            IJobTransactionDbText jobTransactionText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    jobTransactionText = new DfJobTransactionText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    break;
                default:
                    jobTransactionText = new DfJobTransactionText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    break;
            }

            return jobTransactionText;
        }
        internal static IMessageDbText CreateDbTextMessage(IConfigurationDatabase config)
        {
            IMessageDbText messageText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    messageText = new DfMessageText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    break;
                default:
                    messageText = new DfMessageText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    break;
            }

            return messageText;
        }
        internal static IMessageResultDbText CreateDbTextMessageResult(IConfigurationDatabase config)
        {
            IMessageResultDbText messageResultText;

            switch (config.DbTextFactory)
            {
                case TextFactoryEnum.Default:
                    messageResultText = new DfMessageResultText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
                default:
                    messageResultText = new DfMessageResultText(config.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
            }

            return messageResultText;
        }
    }
}
