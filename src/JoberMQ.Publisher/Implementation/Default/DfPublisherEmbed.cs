using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
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