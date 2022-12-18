using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoberMQ.Data.Abstraction.Repository.DbText
{
    internal interface IDbTextRepository<T> where T : DboPropertyGuidBase, new()
    {
        int ArsiveFileCounter { get; set; }
        bool CreateDatabase();
        bool Setup();
        bool WriteLine(string message);
        bool WriteLine(T message);
        List<T> ReadAllData(bool isFullFileList);
        (List<T> datas, List<DataLogFileModel> paths) ReadAllData2(bool isFullFileList);
        List<T> ReadAllDataGrouping(bool isFullFileList);
        (List<T> datas, List<DataLogFileModel> paths) ReadAllDataGrouping2(bool isFullFileList);
        bool DataGroupingAndSize();
        string GetBaseFileFullPath();
        string GetArsiveFileFullPath(int fileNumber);
        FileStream FileStreamCreate(string pathFull, int? bufferSize);
        StreamWriter StreamWriterCreate(FileStream fileStream);
    }
}
