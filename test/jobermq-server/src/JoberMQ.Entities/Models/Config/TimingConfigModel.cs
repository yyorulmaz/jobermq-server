using JoberMQ.Entities.Enums.Schedule;
using JoberMQ.Entities.Enums.Timing;
using JoberMQ.Entities.Constants;

namespace JoberMQ.Entities.Models.Config
{
    public class TimingConfigModel
    {
        internal ScheduleFactoryEnum ScheduleFactory => ServerConst.Timing.ScheduleFactory;
        internal TimingFactoryEnum TimingFactory => ServerConst.Timing.TimingFactory;
    }
}
