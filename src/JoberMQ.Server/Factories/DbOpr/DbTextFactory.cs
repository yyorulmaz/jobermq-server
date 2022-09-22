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
            IJobDataTextDal JobDataTextDal,
            IJobTextDal JobTextDal,
            IMessageTextDal MessageTextDal,
            IMessageResultTextDal MessageResultTextDal)
            CreateDbTexts(DbTextConfigModel dbTextConfig)
        {
            IUserTextDal userTextDal;
            IDistributorTextDal distributorTextDal;
            IQueueTextDal queueTextDal;
            IEventSubTextDal eventSubTextDal;
            IJobDataTextDal jobDataTextDal;
            IJobTextDal jobTextDal;
            IMessageTextDal messageTextDal;
            IMessageResultTextDal messageResultTextDal;

            switch (dbTextConfig.DbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    userTextDal = new DfUserTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x=>x.Key == "User").Value);
                    distributorTextDal = new DfDistributorTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueTextDal = new DfQueueTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubTextDal = new DfEventSubTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobDataTextDal = new DfJobDataTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobData").Value);
                    jobTextDal = new DfJobTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    messageTextDal = new DfMessageTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultTextDal = new DfMessageResultTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
                default:
                    userTextDal = new DfUserTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
                    distributorTextDal = new DfDistributorTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
                    queueTextDal = new DfQueueTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
                    eventSubTextDal = new DfEventSubTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
                    jobDataTextDal = new DfJobDataTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobData").Value);
                    jobTextDal = new DfJobTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
                    messageTextDal = new DfMessageTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
                    messageResultTextDal = new DfMessageResultTextDal(dbTextConfig.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
                    break;
            }

            return (userTextDal, distributorTextDal, queueTextDal, eventSubTextDal, jobDataTextDal, jobTextDal, messageTextDal, messageResultTextDal);
        }
    }
}
