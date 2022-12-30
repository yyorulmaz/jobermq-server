using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;

namespace JoberMQ.Timing.Implementation
{
    internal abstract class TimingBase : ITiming
    {
        protected readonly IMessageBroker messageBroker;
        protected readonly IDatabaseService databaseService;
        protected readonly ISchedule schedule;
        public TimingBase(IDatabaseService databaseService)
        {
            //this.messageBroker = messageBroker;
            this.databaseService = databaseService;
        }
        public TimingBase(IMessageBroker messageBroker, IDatabaseService databaseService)
        {
            this.messageBroker = messageBroker;
            this.databaseService = databaseService;
        }
        public TimingBase(IMessageBroker messageBroker, IDatabaseService databaseService, ISchedule schedule)
        {
            this.messageBroker = messageBroker;
            this.databaseService = databaseService;
            this.schedule = schedule;
        }


        public abstract JobAddResponseModel Timing(JobDbo job);
    }
}
