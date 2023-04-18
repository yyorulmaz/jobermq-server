using JoberMQ.Broker.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Timing;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Factories;
using System.Threading.Tasks;

namespace JoberMQ.Publisher.Implementation.Default
{
    internal sealed class DfPublisherStandart : PublisherBase
    {
        public DfPublisherStandart(IConfiguration configuration, IDatabase database, ISchedule schedule, IMessageBroker messageBroker, IStatusCode statusCode) : base(configuration, database, schedule, messageBroker, statusCode)
        {
        }

        public override async Task<ResponseModel> Publish(JobDbo job)
            => await TimingFactory.CreateTiming(configuration.ConfigurationTiming.TimingFactory, job.Timing.TimingType, messageBroker, database, schedule, statusCode).Timing(job);
    }
}
