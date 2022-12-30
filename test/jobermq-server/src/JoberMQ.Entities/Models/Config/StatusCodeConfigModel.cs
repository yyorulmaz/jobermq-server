using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.StatusCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class StatusCodeConfigModel
    {
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage => ServerConst.StatusCode.StatusCodeMessageLanguage;
    }
}
