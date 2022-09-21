using JoberMQ.Entities.Enums.Config;
using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Implementation.Config.Default;

namespace JoberMQ.Server.Factories.Config
{
    internal class StatusCodeConfigFactory
    {
        public static IStatusCodeConfig CreateStatusCodeConfig(StatusCodeConfigFactoryEnum statusCodeConfigFactory)
        {
            IStatusCodeConfig statusCodeConfig;

            switch (statusCodeConfigFactory)
            {
                case StatusCodeConfigFactoryEnum.Default:
                    statusCodeConfig = new DfStatusCodeConfig();
                    break;
                default:
                    statusCodeConfig = new DfStatusCodeConfig();
                    break;
            }

            return statusCodeConfig;
        }
    }
}
