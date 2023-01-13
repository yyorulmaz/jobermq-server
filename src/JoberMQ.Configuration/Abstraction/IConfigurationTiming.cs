using JoberMQ.Common.Enums.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationTiming
    {
        public ScheduleFactoryEnum ScheduleFactory { get; set; }
        public TimingFactoryEnum TimingFactory { get; set; }
    }
}
