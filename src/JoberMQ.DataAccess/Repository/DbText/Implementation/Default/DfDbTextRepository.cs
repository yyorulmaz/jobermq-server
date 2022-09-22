using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Models.Data;
using JoberMQ.Entities.Models.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace JoberMQ.DataAccess.Repository.DbText.Implementation
{
    internal class DfDbTextRepository<T> : IDbTextRepository<T>
         where T : DboPropertyGuidBase, new()
    {
        private bool isSetup;
        private FileStream activeFileStream;
        private StreamWriter activeStreamWriter;
        private Mutex mutex;
        private int rowCounter = 1;
        private int arsiveFileCounter = 1;

        readonly DbTextFileConfigModel dbTextFileConfig;
        private string baseFileFullPath;
        public DfDbTextRepository(DbTextFileConfigModel dbTextFileConfig)
        {
            this.dbTextFileConfig = dbTextFileConfig;
            baseFileFullPath = GetBaseFileFullPath();
        }

        public DbTextFileConfigModel DbTextFileConfig => dbTextFileConfig;

        public bool Setup()
        {
            try
            {
                if (isSetup)
                    return true;

                if (!Directory.Exists(dbTextFileConfig.DbPath))
                    Directory.CreateDirectory(dbTextFileConfig.DbPath);

                if (!Directory.Exists(Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath })))
                    Directory.CreateDirectory(Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath }));

                CreateActiveFileStream();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void CreateActiveFileStream()
        {
            mutex = new Mutex(false, dbTextFileConfig.DbFileName);
            int bufferSize = 32768;
            FileShare fileShare = FileShare.ReadWrite | FileShare.Delete;

            activeFileStream = new FileStream(
                baseFileFullPath,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                fileShare,
                bufferSize);

            activeStreamWriter = new StreamWriter(activeFileStream);
        }

        public bool WriteLine(string message)
        {
            try
            {
                if (rowCounter == dbTextFileConfig.MaxRowCount)
                {
                    try
                    {
                        File.Move(baseFileFullPath, GetArsiveFileFullPath(arsiveFileCounter));

                        CreateActiveFileStream();

                        rowCounter = 1;
                        arsiveFileCounter++;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }

                lock (mutex)
                {
                    activeStreamWriter.WriteLine(message);
                    activeStreamWriter.Flush();
                }

                rowCounter++;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool WriteLine(T message)
        {
            try
            {
                if (rowCounter == dbTextFileConfig.MaxRowCount)
                {
                    try
                    {
                        File.Move(baseFileFullPath, GetArsiveFileFullPath(arsiveFileCounter));

                        CreateActiveFileStream();

                        rowCounter = 1;
                        arsiveFileCounter++;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }

                lock (mutex)
                {
                    activeStreamWriter.WriteLine(JsonConvert.SerializeObject(message, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                    activeStreamWriter.Flush();
                }

                rowCounter++;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<T> ReadAll(bool isFullFileList)
        {
            List<T> list = new List<T>();

            List<DataLogFileModel> fullFileList;
            if (isFullFileList)
                fullFileList = GetFullFileList();
            else
                fullFileList = GetArsiveFileList();

            if (fullFileList == null)
                return null;

            foreach (var item in fullFileList)
            {
                string row;
                StreamReader sr = new StreamReader(item.FullPath, Encoding.UTF8);

                while ((row = sr.ReadLine()) != null)
                {
                    var rowData = JsonConvert.DeserializeObject<T>(row);
                    list.Add(rowData);
                }
                sr.Close();
            }

            return list;
        }
        public List<T> ReadAllGrouping(bool isFullFileList)
        {
            var datas = ReadAll(isFullFileList);

            if (datas == null)
                return null;

            var groupDatas = datas
                .GroupBy(g => g.Id)
                .Select(g => g.OrderByDescending(t => t.DataStatusType).OrderByDescending(t => t.ProcessTime).FirstOrDefault())
                .ToList();

            return groupDatas;
        }

        private List<DataLogFileModel> GetArsiveFileList()
        {
            var fileList = new List<DataLogFileModel>();
            string[] files = Directory.GetFiles(Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath }));
            foreach (var item in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(item);
                var dataCheck = new DataLogFileModel();
                dataCheck.FullPath = item;

                if (fileName != dbTextFileConfig.DbFileName)
                {
                    dataCheck.FileName = fileName.Split(dbTextFileConfig.DbFileSeparator)[0];
                    dataCheck.Number = Convert.ToInt32(fileName.Split(dbTextFileConfig.DbArchiveFileSeparator)[1]);
                    fileList.Add(dataCheck);
                }
            }

            return fileList;
        }
        private List<DataLogFileModel> GetFullFileList()
        {
            var fileList = new List<DataLogFileModel>();
            var checkFolder = Directory.Exists(Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath }));
            if (checkFolder == false)
                return null;

            string[] files = Directory.GetFiles(Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath }));
            foreach (var item in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(item);
                var dataCheck = new DataLogFileModel();
                dataCheck.FullPath = item;

                if (fileName == dbTextFileConfig.DbFileName)
                {
                    dataCheck.FileName = fileName;
                    dataCheck.Number = 2000000000;
                }
                else
                {

                    dataCheck.FileName = fileName.Split(dbTextFileConfig.DbFileSeparator)[0];
                    dataCheck.Number = Convert.ToInt32(fileName.Split(dbTextFileConfig.DbArchiveFileSeparator)[1]);
                }


                fileList.Add(dataCheck);
            }

            return fileList;
        }



        private string GetBaseFileFullPath()
            => Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath, dbTextFileConfig.DbFileName + dbTextFileConfig.DbFileSeparator + dbTextFileConfig.DbFileExtension });
        private string GetArsiveFileFullPath(int fileNumber)
            => Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath, dbTextFileConfig.DbFileName + "_" + fileNumber + dbTextFileConfig.DbFileSeparator + dbTextFileConfig.DbFileExtension });

        public bool DataClearAndSize(bool isStarted)
        {
            //tüm datayı aldım
            var datas = ReadAllGrouping(true);

            //tüm dosyaları aldım
            var fullFileList = GetFullFileList();


            if (fullFileList.FirstOrDefault(x => x.FileName == dbTextFileConfig.DbFileName + "_" + "temp") == null)
            {
                File.Move(
                        baseFileFullPath,
                        Path.Combine(new string[] { dbTextFileConfig.DbPath, dbTextFileConfig.DbFolderPath, dbTextFileConfig.DbFileName + "_" + "0" + dbTextFileConfig.DbFileSeparator + dbTextFileConfig.DbFileExtension }));

            }




            return true;
        }
    }
}
