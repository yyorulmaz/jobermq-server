using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Entities.Enums.Jober;
using JoberMQ.Implementation.Jober.Default;

namespace JoberMQ.Factories.Jober
{
    internal class JoberFactory
    {
        internal static IJober CreateJober(JoberFactoryEnum factory, IConfiguration configuration)
        {
            IJober jober;

            switch (factory)
            {
                case JoberFactoryEnum.Default:
                    jober = new DfJober(configuration);
                    break;
                default:
                    jober = new DfJober(configuration);
                    break;
            }

            return jober;
        }
    }
}
