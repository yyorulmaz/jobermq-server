using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Factories.Timing;

namespace JoberMQ.Implementation.Publisher.Default
{
    internal class DefaultPublisherStandart : PublisherBase
    {
        public override async Task<ResponseModel> Publish(JobDbo job)
            => await TimingFactory.CreateTiming(JoberHost.JoberMQ.Configuration.ConfigurationTiming.TimingFactory, job.Timing.TimingType).Timing(job);

    }
}
