using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;
using System.Collections.Concurrent;

namespace JoberMQ.Entities.Models.Config
{
    public class ServerConfigModel
    {
        internal ServerFactoryEnum ServerFactory => ServerConst.ServerFactory;
        internal ClientFactoryEnum ClientFactory => ServerConst.ClientFactory;
        internal ClientServiceFactoryEnum ClientServiceFactory => ServerConst.ClientServiceFactory;

        public StatusCodeConfigModel StatusCodeConfig => new StatusCodeConfigModel();
        public SecurityConfigModel SecurityConfig => new SecurityConfigModel();
        public DbOprConfigModel DbOprConfig => new DbOprConfigModel();
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
        internal DbMemConfigModel DbMemConfig => new DbMemConfigModel();
        internal DbTextConfigModel DbTextConfig => new DbTextConfigModel();

    }
    public class DbMemConfigModel
    {
        internal DbMemFactoryEnum DbMemFactory => ServerConst.DbOpr.DbMemFactory;
        internal DbMemDataFactoryEnum DbMemDataFactory => ServerConst.DbOpr.DbMemDataFactory;
    }
    public class DbTextConfigModel
    {
        internal DbTextFactoryEnum DbTextFactory => ServerConst.DbOpr.DbTextFactory;
        internal ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas = ServerConst.DbOpr.DbTextFileConfigDatas;
    }
    public class DbTextFileConfigModel
    {
        public string DbPath { get; set; }
        public string DbFolderPath { get; set; }
        public string DbFileName { get; set; }
        public char DbFileSeparator { get; set; }
        public char DbArchiveFileSeparator { get; set; }
        public string DbFileExtension { get; set; }
        public int MaxRowCount { get; set; }
    }
}
