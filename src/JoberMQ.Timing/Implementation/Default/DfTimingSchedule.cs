using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;
using TimerFramework;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTimingSchedule : TimingBase
    {
        public DfTimingSchedule(IMessageBroker messageBroker, IDatabase database, ISchedule schedule) : base(messageBroker, database, schedule)
        {
        }

        public override JobAddResponseModel Timing(JobDbo job)
        {
            // todo kontrol et, delayed ve recurrent için ayrım yapmadım. ikisinide cron time üzerinden bastım
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

            var addJobResult = database.Job.Add(job.Id, job);

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
