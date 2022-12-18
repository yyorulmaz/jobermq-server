using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.DataAccess.Implementation.DbText.Default;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
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
            CreateDbTexts(DbTextConfigModel dbTextConfig)
        {
            IUserTextDal userTextDal;
            IDistributorTextDal distributorTextDal;
            IQueueTextDal queueTextDal;
            IEventSubTextDal eventSubTextDal;
            IJobTextDal jobTextDal;
            IJobTransactionTextDal jobTransactionTextDal;
            IMessageTextDal messageTextDal;
            IMessageResultTextDal messageResultTextDal;

            switch (dbTextConfig.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    userTextDal = new DfUserTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x=>x.Key == "User").Value);
                    distributorTextDal = new DfDistributorTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueTextDal = new DfQueueTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubTextDal = new DfEventSubTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobTextDal = new DfJobTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    jobTransactionTextDal = new DfJobTransactionTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    messageTextDal = new DfMessageTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultTextDal = new DfMessageResultTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
                default:
                    userTextDal = new DfUserTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    distributorTextDal = new DfDistributorTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueTextDal = new DfQueueTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubTextDal = new DfEventSubTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobTextDal = new DfJobTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    jobTransactionTextDal = new DfJobTransactionTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
                    messageTextDal = new DfMessageTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultTextDal = new DfMessageResultTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
            }

            return (userTextDal, distributorTextDal, queueTextDal, eventSubTextDal, jobTextDal, jobTransactionTextDal, messageTextDal, messageResultTextDal);
        }
    }
}
