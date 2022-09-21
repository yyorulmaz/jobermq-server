using JoberMQ.Entities.Enums.Config;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;

namespace JoberMQ.Entities.Constants
{
    internal class ServerConst
    {
        internal const ServerFactoryEnum ServerFactory = ServerFactoryEnum.Default;

        internal class Config
        {
            internal const ServerConfigFactoryEnum ServerConfigFactory = ServerConfigFactoryEnum.Default;

            internal const SecurityConfigFactoryEnum SecurityConfigFactory = SecurityConfigFactoryEnum.Default;
            internal const string SecurityKey = "böyle_bir_aşk_görülmemiş_dünyada_ne_geçmişte_nede_bundan_sonrada";

            internal const StatusCodeConfigFactoryEnum StatusCodeConfigFactory = StatusCodeConfigFactoryEnum.Default;
            internal const StatusCodeMessageLanguageEnum StatusCodeMessageLanguage = StatusCodeMessageLanguageEnum.en;
        }
    }
}
