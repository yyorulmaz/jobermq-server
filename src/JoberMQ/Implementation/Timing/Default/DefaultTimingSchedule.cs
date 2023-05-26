using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using TimerFramework;

namespace JoberMQ.Implementation.Timing.Default
{
    internal class DefaultTimingSchedule : TimingBase
    {
        public override async Task<ResponseModel> Timing(JobDbo job)
        {
            // todo kontrol et, delayed ve recurrent için ayrım yapmadım. ikisinide cron time üzerinden bastım
            var response = new ResponseModel();
            response.IsOnline = true;
            response.Id = job.Id;

            var addJobResult = JoberHost.JoberMQ.Database.Job.Add(job.Id, job);

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

            var timerResult = JoberHost.JoberMQ.Schedule.JobTimer.Add(timer);

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
