using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;
using TimerFramework;

namespace JoberMQ.Server.Implementation.Timing.Default
{
    internal class DfTimingSchedule : TimingBase
    {
        public DfTimingSchedule(IDbOprService dbOprService, ISchedule schedule) : base(dbOprService, schedule)
        {
        }

        public override JobDataAddResponseModel Timing(JobDataDbo jobData)
        {
            // todo kontrol et, delayed ve recurrent için ayrım yapmadım. ikisinide cron time üzerinden bastım
            var response = new JobDataAddResponseModel();
            response.IsOnline = true;

            dbOprService.JobData.Add(jobData);

            var timer = new TimerModel();
            timer.Id = jobData.Id;
            timer.CronTime = jobData.CronTime;
            timer.TimerGroup = "jobScheduleData";
            //timer.Data = JsonConvert.SerializeObject(jobScheduleDbo);

            var timerResult = schedule.JobDataTimer.Add(timer);

            response.IsSuccess = true;
            response.JobId = jobData.Id;
            return response;
        }
    }
}
