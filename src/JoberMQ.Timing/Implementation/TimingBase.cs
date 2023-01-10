using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;

namespace JoberMQ.Timing.Implementation
{
    internal abstract class TimingBase : ITiming
    {
        protected readonly IMessageBroker messageBroker;
        protected readonly IDatabase database;
        protected readonly ISchedule schedule;
        public TimingBase(IDatabase database)
        {
            //this.messageBroker = messageBroker;
            this.database = database;
        }
        public TimingBase(IMessageBroker messageBroker, IDatabase database)
        {
            this.messageBroker = messageBroker;
            this.database = database;
        }
        public TimingBase(IMessageBroker messageBroker, IDatabase database, ISchedule schedule)
        {
            this.messageBroker = messageBroker;
            this.database = database;
            this.schedule = schedule;
        }


        public abstract JobAddResponseModel Timing(JobDbo job);
    }
}
