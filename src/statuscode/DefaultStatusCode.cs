using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Enums.StatusCode;
using JoberMQ.Entities.Models.StatusCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

internal class DefaultStatusCode : ConcurrentDictionaryRepository<string, StatusCodeModel>, IStatusCode
{
    private StatusCodeMessageLanguageEnum statusCodeMessageLanguage;
    public DefaultStatusCode(StatusCodeMessageLanguageEnum statusCodeMessageLanguage) => this.statusCodeMessageLanguage = statusCodeMessageLanguage;

    public bool Start()
    {
        try
        {
            var ass = Assembly.GetAssembly(typeof(DefaultStatusCode));
            //var data = ResourceFileHelper.GetString(ass, @"StatusCode", "StatusCodes.json");
            var data = ResourceFileHelper.GetString(ass, null, "StatusCodes.json");
            var deserialize = JsonConvert.DeserializeObject<List<StatusCodeModel>>(data);

            foreach (var item in deserialize)
                Add(item.StatusCode, item);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string GetStatusMessage(string statusCode) => GetStatusMessage(statusCode, statusCodeMessageLanguage);
    public string GetStatusMessage(string statusCode, StatusCodeMessageLanguageEnum language)
    {
        try
        {
            var status = Get(statusCode);

            var message = status.StatusCodeMessages.FirstOrDefault(x => x.Language == language).Message;
            return $"{status.StatusCode} - {message}";
        }
        catch (Exception)
        {
            return null;
        }
    }
}