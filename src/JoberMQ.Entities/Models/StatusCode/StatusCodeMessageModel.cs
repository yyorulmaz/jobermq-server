using JoberMQ.Entities.Enums.StatusCode;

namespace JoberMQ.Entities.Models.StatusCode
{
    public class StatusCodeMessageModel
    {
        public StatusCodeMessageLanguageEnum Language { get; set; }
        public string Message { get; set; }
    }
}
