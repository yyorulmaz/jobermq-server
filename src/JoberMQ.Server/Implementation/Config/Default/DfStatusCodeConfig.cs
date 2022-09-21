using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.StatusCode;
using JoberMQ.Server.Abstraction.Config;
using System;

namespace JoberMQ.Server.Implementation.Config.Default
{
    internal class DfStatusCodeConfig: IStatusCodeConfig
    {
        StatusCodeMessageLanguageEnum statusCodeMessageLanguage = ServerConst.Config.StatusCodeMessageLanguage;
        StatusCodeMessageLanguageEnum IStatusCodeConfig.StatusCodeMessageLanguage => statusCodeMessageLanguage;

        public bool SetStatusCodeConfig(StatusCodeMessageLanguageEnum statusCodeMessageLanguage)
        {
            try
            {
                this.statusCodeMessageLanguage = statusCodeMessageLanguage;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
