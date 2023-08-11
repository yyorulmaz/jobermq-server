using System.Collections.Concurrent;
using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationStatusCode : IConfigurationStatusCode
    {
        StatusCodeFactoryEnum statusCodeFactory = ConfigurationStatusCodeConst.StatusCodeFactory;
        public StatusCodeFactoryEnum StatusCodeFactory { get => statusCodeFactory; set => statusCodeFactory = value; }
        StatusCodeMessageLanguageEnum statusCodeMessageLanguage = ConfigurationStatusCodeConst.StatusCodeMessageLanguage;
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get => statusCodeMessageLanguage; set => statusCodeMessageLanguage = value; }
        ConcurrentDictionary<string, StatusCodeModel> statusCodeDatas = ConfigurationStatusCodeConst.DefaultStatusCodeDatas;
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get => statusCodeDatas; set => statusCodeDatas = value; }
    }
}
