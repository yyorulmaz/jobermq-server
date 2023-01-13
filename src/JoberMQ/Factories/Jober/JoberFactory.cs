using JoberMQ.Abstraction.Jober;
using JoberMQ.Common.Enums.Jober;
using JoberMQ.Configuration.Abstraction;
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
