using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationStatusCode
    {
        public StatusCodeFactoryEnum StatusCodeFactory { get; set; }
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get; set; }
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get; set; }
    }
}
