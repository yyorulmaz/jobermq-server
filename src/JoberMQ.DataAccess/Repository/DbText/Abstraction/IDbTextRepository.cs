using JoberMQ.Entities.Base.Dbo;
using System.Collections.Generic;

namespace JoberMQ.DataAccess.Repository.DbText.Abstraction
{
    internal interface IDbTextRepository<T> where T : DboPropertyGuidBase, new()
    {
        bool Setup();
        bool WriteLine(string message);
        bool WriteLine(T message);
        List<T> ReadAllData(bool isFullFileList);
        List<T> ReadAllDataGrouping(bool isFullFileList);
        bool DataGroupingAndSize();
    }
}
