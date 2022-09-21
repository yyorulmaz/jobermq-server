using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Enums.StatusCode;
using JoberMQ.Entities.Models.StatusCode;

internal interface IStatusCode : IConcurrentDictionaryRepository<string, StatusCodeModel>
{
    bool Start();
    string GetStatusMessage(string statusCode);
    string GetStatusMessage(string statusCode, StatusCodeMessageLanguageEnum language);
}
