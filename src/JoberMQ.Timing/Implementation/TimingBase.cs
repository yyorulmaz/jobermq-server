using JoberMQ.Broker.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Timing.Abstraction;
using System.Threading.Tasks;

namespace JoberMQ.Timing.Implementation
{
    internal abstract class TimingBase : ITiming
    {
        protected readonly IMessageBroker messageBroker;
        protected readonly IDatabase database;
        protected readonly ISchedule schedule;
        protected readonly IStatusCode statusCode;
        public TimingBase(IDatabase database, IStatusCode statusCode)
        {
            //this.messageBroker = messageBroker;
            this.database = database;
            this.statusCode = statusCode;
        }
        public TimingBase(IMessageBroker messageBroker, IDatabase database, IStatusCode statusCode)
        {
            this.messageBroker = messageBroker;
            this.database = database;
            this.statusCode = statusCode;
        }
        public TimingBase(IMessageBroker messageBroker, IDatabase database, ISchedule schedule, IStatusCode statusCode)
        {
            this.messageBroker = messageBroker;
            this.database = database;
            this.schedule = schedule;
            this.statusCode = statusCode;
        }


        public abstract Task<ResponseModel> Timing(JobDbo job);
    }
}
