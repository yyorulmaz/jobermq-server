using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;
using TimerFramework;

namespace JoberMQ.Server.Implementation.Timing.Default
{
    internal class DfTimingSchedule : TimingBase
    {
        public DfTimingSchedule(IBroker broker, IDbOprService dbOprService, ISchedule schedule) : base(broker, dbOprService, schedule)
        {
        }

        public override JobAddResponseModel Timing(JobDbo job)
        {
            // todo kontrol et, delayed ve recurrent için ayrım yapmadım. ikisinide cron time üzerinden bastım
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

            var addJobResult = dbOprService.Job.Add(job);

            if (!addJobResult)
            {
                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            var timer = new TimerModel();
            timer.Id = job.Id;
            timer.CronTime = job.CronTime;
            timer.TimerGroup = "jobScheduleData";
            //timer.Data = JsonConvert.SerializeObject(jobScheduleDbo);

            var timerResult = schedule.JobTimer.Add(timer);

            if (!timerResult)
            {
                response.IsSuccess = false;
                response.Message = "Timer eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}
