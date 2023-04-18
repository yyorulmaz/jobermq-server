using JoberMQ.Broker.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Timing.Abstraction;
using System.Threading.Tasks;
using TimerFramework;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTimingSchedule : TimingBase
    {
        public DfTimingSchedule(IMessageBroker messageBroker, IDatabase database, ISchedule schedule, IStatusCode statusCode) : base(messageBroker, database, schedule, statusCode)
        {
        }

        public override async Task<ResponseModel> Timing(JobDbo job)
        {
            // todo kontrol et, delayed ve recurrent için ayrım yapmadım. ikisinide cron time üzerinden bastım
            var response = new ResponseModel();
            response.IsOnline = true;
            response.Id = job.Id;

            var addJobResult = database.Job.Add(job.Id, job);

            if (!addJobResult)
            {
                response.IsSucces = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            var timer = new TimerModel();
            timer.Id = job.Id;
            timer.CronTime = job.Timing.CronTime;
            timer.TimerGroup = "jobScheduleData";
            //timer.Data = JsonConvert.SerializeObject(jobScheduleDbo);

            var timerResult = schedule.JobTimer.Add(timer);

            if (!timerResult)
            {
                response.IsSucces = false;
                response.Message = "Timer eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            response.IsSucces = true;
            return response;
        }
    }
}
