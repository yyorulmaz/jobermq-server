﻿using JoberMQ.Common.Enums.Broker;
using JoberMQ.Common.Enums.Timing;

namespace JoberMQ.Timing.Abstraction
{
    public interface IConfigurationTiming
    {
        public ScheduleFactoryEnum ScheduleFactory { get; set; }
        public TimingFactoryEnum TimingFactory { get; set; }
    }
}
