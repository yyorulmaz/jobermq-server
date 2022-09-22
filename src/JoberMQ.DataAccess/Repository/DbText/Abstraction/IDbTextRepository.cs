using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Models.Config;
using System.Collections.Generic;

namespace JoberMQ.DataAccess.Repository.DbText.Abstraction
{
    internal interface IDbTextRepository<T> where T : DboPropertyGuidBase, new()
    {
        DbTextFileConfigModel DbTextFileConfig { get; }
        bool Setup();
        bool WriteLine(string message);
        bool WriteLine(T message);
        List<T> ReadAll(bool isFullFileList);
        List<T> ReadAllGrouping(bool isFullFileList);
    }
}
