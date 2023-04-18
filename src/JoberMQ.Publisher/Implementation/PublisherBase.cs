using JoberMQ.Broker.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Publisher.Abstraction;
using JoberMQ.Timing.Abstraction;
using System.Threading.Tasks;

namespace JoberMQ.Publisher.Implementation.Default
{
    internal abstract class PublisherBase : IPublisher
    {
        protected readonly IConfiguration configuration;
        protected readonly IDatabase database;
        protected readonly ISchedule schedule;
        protected readonly IMessageBroker messageBroker;
        protected readonly IStatusCode statusCode;
        public PublisherBase(IConfiguration configuration, IDatabase database, IStatusCode statusCode)
        {
            this.configuration = configuration;
            this.database = database;
            this.statusCode = statusCode;
        }
        public PublisherBase(IConfiguration configuration, IDatabase database, ISchedule schedule, IMessageBroker messageBroker, IStatusCode statusCode)
        {
            this.configuration = configuration;
            this.database = database;
            this.schedule = schedule;
            this.messageBroker = messageBroker;
            this.statusCode = statusCode;
        }

        public abstract Task<ResponseModel> Publish(JobDbo job);
    }
}
