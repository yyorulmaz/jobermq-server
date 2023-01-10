using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.StatusCode.Enums;
using JoberMQ.Library.StatusCode.Models;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationStatusCode : IConfigurationStatusCode
    {
        StatusCodeFactoryEnum statusCodeFactory = DefaultStatusCodeConst.StatusCodeFactory;
        public StatusCodeFactoryEnum StatusCodeFactory { get => statusCodeFactory; set => statusCodeFactory = value; }
        StatusCodeMessageLanguageEnum statusCodeMessageLanguage = DefaultStatusCodeConst.StatusCodeMessageLanguage;
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get => statusCodeMessageLanguage; set => statusCodeMessageLanguage = value; }
        ConcurrentDictionary<string, StatusCodeModel> statusCodeDatas = DefaultStatusCodeConst.DefaultStatusCodeDatas;
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get => statusCodeDatas; set => statusCodeDatas = value; }
    }
}
