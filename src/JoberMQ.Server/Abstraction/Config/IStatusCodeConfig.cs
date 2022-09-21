using JoberMQ.Entities.Enums.StatusCode;

namespace JoberMQ.Server.Abstraction.Config
{
    public interface IStatusCodeConfig
    {
        internal StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get; }
        public bool SetStatusCodeConfig(StatusCodeMessageLanguageEnum statusCodeMessageLanguage);
    }
}
