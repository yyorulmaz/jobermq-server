using JoberMQ.Entities.Enums.StatusCode;

internal class StatusCodeFactory
{
    internal static IStatusCode CreateStatusCodeService(StatusCodeMessageLanguageEnum statusCodeMessageLanguage)
    {
        IStatusCode statusCodeService = new DefaultStatusCode(statusCodeMessageLanguage);
        return statusCodeService;
    }
}
