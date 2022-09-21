using JoberMQ.Entities.Base.Dbo;
using System.Collections.Generic;

namespace JoberMQ.DataAccess.Repository.DbText.Abstraction
{
    internal interface IDbTextRepository<T> where T : DboPropertyGuidBase, new()
    {
        string DbPath { get; }
        string DbFolderPath { get; }
        string DbFileName { get; }
        char DbFileSeparator { get; }
        char DbArchiveFileSeparator { get; }
        string DbFileExtension { get; }
        bool Setup();
        bool WriteLine(string message);
        bool WriteLine(T message);
        List<T> ReadAll(bool isStarted);
        List<T> ReadAllGroup(bool isStarted);
    }
}
