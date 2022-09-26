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

        public override JobDataAddResponseModel Timing(JobDataDbo jobData)
        {
            // todo kontrol et, delayed ve recurrent için ayrım yapmadım. ikisinide cron time üzerinden bastım
            var response = new JobDataAddResponseModel();
            response.IsOnline = true;
            response.JobId = jobData.Id;

            var addJobDataResult = dbOprService.JobData.Add(jobData);

            if (!addJobDataResult)
            {
                response.IsSuccess = false;
                response.Message = "JobData eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            var timer = new TimerModel();
            timer.Id = jobData.Id;
            timer.CronTime = jobData.CronTime;
            timer.TimerGroup = "jobScheduleData";
            //timer.Data = JsonConvert.SerializeObject(jobScheduleDbo);

            var timerResult = schedule.JobDataTimer.Add(timer);

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
