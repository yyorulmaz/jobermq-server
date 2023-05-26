using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation;
using JoberMQ.Common.Enums;

namespace JoberMQ.Factories
{
    internal class JoberMQFactory
    {
        internal static IJoberMQ Create(JoberMQFactoryEnum joberMQFactory, IConfiguration configuration)
        {
            IJoberMQ result;

            switch (joberMQFactory)
            {
                case JoberMQFactoryEnum.Default:
                    result = new DefaultJoberMQ(configuration);
                    break;
                default:
                    result = new DefaultJoberMQ(configuration);
                    break;
            }

            return result;
        }
    }
}
