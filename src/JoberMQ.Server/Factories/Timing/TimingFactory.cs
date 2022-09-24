using JoberMQ.Entities.Enums.Timing;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;
using JoberMQ.Server.Implementation.Timing.Default;

namespace JoberMQ.Server.Factories.Timing
{
    internal class TimingFactory
    {
        internal static ITiming CreateTiming(TimingFactoryEnum timingFactory, TimingTypeEnum timingType, IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            ITiming timing;

            switch (timingFactory)
            {
                case TimingFactoryEnum.Default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(dbOprService, dboCreator);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(dbOprService, schedule);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTiminTrigger(dbOprService);
                            break;
                        default:
                            timing = new DfTiminTrigger(dbOprService);
                            break;
                    }
                    break;
                default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(dbOprService, dboCreator);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(dbOprService, schedule);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTiminTrigger(dbOprService);
                            break;
                        default:
                            timing = new DfTiminTrigger(dbOprService);
                            break;
                    }
                    break;
            }

            return timing;
        }
    }
}
