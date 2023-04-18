using JoberMQ.Abstraction.Jober;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Implementation.Jober.Default;
using JoberMQ.Library.Enums.Jober;

namespace JoberMQ.Factories.Jober
{
    internal class JoberFactory
    {
        public static IJober Create(JoberFactoryEnum joberFactory, IConfiguration configuration)
        {
            IJober result;

            switch (joberFactory)
            {
                case JoberFactoryEnum.Default:
                    result = new DfJober(configuration);
                    break;
                default:
                    result = new DfJober(configuration);
                    break;
            }

            return result;
        }
    }
}
