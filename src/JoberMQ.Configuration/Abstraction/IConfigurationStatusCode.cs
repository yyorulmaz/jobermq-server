using JoberMQ.Library.StatusCode.Enums;
using JoberMQ.Library.StatusCode.Models;
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
