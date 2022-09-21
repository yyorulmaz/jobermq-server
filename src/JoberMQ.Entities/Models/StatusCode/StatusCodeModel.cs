using JoberMQ.Entities.Enums.StatusCode;
using System.Collections.Generic;

namespace JoberMQ.Entities.Models.StatusCode
{
    public class StatusCodeModel
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusCodeTypeEnum StatusCodeType { get; set; }
        public string StatusCode { get; set; }
        public List<StatusCodeMessageModel> StatusCodeMessages { get; set; }
    }
}
