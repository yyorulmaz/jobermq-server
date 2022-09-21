using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;

namespace JoberMQ.Entities.Constants
{
    internal class ServerConst
    {
        internal const ServerFactoryEnum ServerFactory = ServerFactoryEnum.Default;

        internal class StatusCode
        {
            internal const StatusCodeMessageLanguageEnum StatusCodeMessageLanguage = StatusCodeMessageLanguageEnum.en;
        }
        internal class Security
        {
            internal const string SecurityKey = "böyle_bir_aşk_görülmemiş_dünyada_ne_geçmişte_nede_bundan_sonrada";
        }
        internal class DbOpr
        {
            internal const DbOprFactoryEnum DbOprFactory = DbOprFactoryEnum.Default;
            internal const DbOprServiceFactoryEnum DbOprServiceFactory = DbOprServiceFactoryEnum.Default;
            internal const DbMemFactoryEnum DbMemFactory = DbMemFactoryEnum.Default;
            internal const DbMemDataFactoryEnum DbMemDataFactory = DbMemDataFactoryEnum.Data;
            internal const DbTextFactoryEnum DbTextFactory = DbTextFactoryEnum.Default;

        }
    }
}
