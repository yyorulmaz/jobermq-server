using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;

namespace JoberMQ.Entities.Models.Config
{
    public class ServerConfigModel
    {
        internal ServerFactoryEnum ServerFactory => ServerConst.ServerFactory;
        internal ClientFactoryEnum ClientFactory => ServerConst.ClientFactory;
        internal ClientServiceFactoryEnum ClientServiceFactory => ServerConst.ClientServiceFactory;

        public StatusCodeConfigModel StatusCodeConfig { get; set; }
        public SecurityConfigModel SecurityConfig { get; set; }
        public DbOprConfigModel DbOprConfig { get; set; }
    }

    public class StatusCodeConfigModel
    {
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage => ServerConst.StatusCode.StatusCodeMessageLanguage;
    }
    public class SecurityConfigModel
    {
        public string SecurityKey { get; set; }
    }
    public class DbOprConfigModel
    {
        internal DbOprServiceFactoryEnum DbOprServiceFactory => ServerConst.DbOpr.DbOprServiceFactory;
        internal DbOprFactoryEnum DbOprFactory => ServerConst.DbOpr.DbOprFactory;
        public DbMemConfigModel DbMemConfig { get; set; }
        public DbTextConfigModel DbTextConfig { get; set; }

    }
    public class DbMemConfigModel
    {
        internal DbMemFactoryEnum DbMemFactory => ServerConst.DbOpr.DbMemFactory;
        internal DbMemDataFactoryEnum DbMemDataFactory => ServerConst.DbOpr.DbMemDataFactory;
    }
    public class DbTextConfigModel
    {
        internal DbTextFactoryEnum DbTextFactory => ServerConst.DbOpr.DbTextFactory;

    }
}
