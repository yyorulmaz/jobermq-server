using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Implementation.Publisher.Default
{
    internal class DefaultPublisherEmbed : PublisherBase
    {
        public override async Task<ResponseModel> Publish(JobDbo job)
        {
            var succes = JoberHost.JoberMQ.Database.Job.Add(job.Id, job);
            return new ResponseModel
            {
                IsOnline = true,
                IsSucces = succes,
                Id = job.Id,
                Message = ""
            };
        }
    }
}
