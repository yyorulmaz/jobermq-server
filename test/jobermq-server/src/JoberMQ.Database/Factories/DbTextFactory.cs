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
    }
}
