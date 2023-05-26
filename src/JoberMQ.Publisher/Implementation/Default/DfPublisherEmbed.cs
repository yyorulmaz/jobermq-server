using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.StatusCode.Abstraction;
using System.Threading.Tasks;

namespace JoberMQ.Publisher.Implementation.Default
{
    internal sealed class DfPublisherEmbed : PublisherBase
    {
        public DfPublisherEmbed(IConfiguration configuration, IDatabase database, IStatusCode statusCode) : base(configuration, database, statusCode)
        {
        }

        public override async Task<ResponseModel> Publish(JobDbo job)
        {
            var jobAddResult = this.database.Job.Add(job.Id, job);
            return new ResponseModel
            {
                IsOnline = true,
                IsSucces = jobAddResult,
                Id = job.Id,
                Message = ""
            };
        }
    }
}