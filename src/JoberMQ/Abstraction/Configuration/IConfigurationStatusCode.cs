using System.Collections.Concurrent;
using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationStatusCode
    {
        public StatusCodeFactoryEnum StatusCodeFactory { get; set; }
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get; set; }
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get; set; }
    }
}
