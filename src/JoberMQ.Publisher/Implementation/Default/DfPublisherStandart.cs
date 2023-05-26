using JoberMQ.Broker.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Database.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.StatusCode.Abstraction;
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
