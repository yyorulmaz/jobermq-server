using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Models.Data;
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
        //private FileStream archiveFileStream;
        private StreamWriter activeStreamWriter;

        //private StreamWriter archiveStreamWriter;
        private Mutex mutex;
        private int rowCounter = 1;
        private int arsiveFileCounter = 1;

        readonly string dbPath;
        readonly string dbFolderPath;
        readonly string dbFileName;
        readonly char dbFileSeparator;
        readonly char dbArchiveFileSeparator;
        readonly string dbFileExtension;
        readonly int maxRowCount;
        public DfDbTextRepository(string dbPath, string dbFolderPath, string dbFileName, char dbFileSeparator, char dbArchiveFileSeparator, string dbFileExtension, int maxRowCount)
        {
            this.dbPath = dbPath;
            this.dbFolderPath = dbFolderPath;
            this.dbFileName = dbFileName;
            this.dbFileSeparator = dbFileSeparator;
            this.dbArchiveFileSeparator = dbArchiveFileSeparator;
            this.dbFileExtension = dbFileExtension;
            this.maxRowCount = maxRowCount;
        }

        public string DbPath => dbPath;
        public string DbFolderPath => dbFolderPath;
        public string DbFileName => dbFileName;
        public char DbFileSeparator => dbFileSeparator;
        public char DbArchiveFileSeparator => dbArchiveFileSeparator;
        public string DbFileExtension => dbFileExtension;
        public int MaxRowCount => maxRowCount;

        public bool Setup()
        {
            try
            {
                if (isSetup)
                    return true;

                if (!Directory.Exists(dbPath))
                    Directory.CreateDirectory(dbPath);

                if (!Directory.Exists(Path.Combine(new string[] { dbPath, dbFolderPath })))
                    Directory.CreateDirectory(Path.Combine(new string[] { dbPath, dbFolderPath }));

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
            mutex = new Mutex(false, dbFileName);
            int bufferSize = 32768;
            FileShare fileShare = FileShare.ReadWrite | FileShare.Delete;

            activeFileStream = new FileStream(
                Path.Combine(new string[] { dbPath, dbFolderPath, dbFileName + dbFileSeparator + dbFileExtension }),
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
                if (rowCounter == maxRowCount)
                {
                    try
                    {
                        File.Move(
                            Path.Combine(new string[] { dbPath, dbFolderPath, dbFileName + dbFileSeparator + dbFileExtension }),
                            Path.Combine(new string[] { dbPath, dbFolderPath, dbFileName + "_" + arsiveFileCounter + dbFileSeparator + dbFileExtension }));

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
                if (rowCounter == maxRowCount)
                {
                    try
                    {
                        File.Move(
                            Path.Combine(new string[] { dbPath, dbFolderPath, dbFileName + dbFileSeparator + dbFileExtension }),
                            Path.Combine(new string[] { dbPath, dbFolderPath, dbFileName + "_" + arsiveFileCounter + dbFileSeparator + dbFileExtension }));

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
        public List<T> ReadAll(bool isStarted)
        {
            List<T> list = new List<T>();

            List<DataLogFileModel> fullFileList;
            if (isStarted)
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
        public List<T> ReadAllGroup(bool isStarted)
        {
            var datas = ReadAll(isStarted);

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
            string[] files = Directory.GetFiles(Path.Combine(new string[] { dbPath, dbFolderPath }));
            foreach (var item in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(item);
                var dataCheck = new DataLogFileModel();
                dataCheck.FullPath = item;

                if (fileName != dbFileName)
                {
                    dataCheck.FileName = fileName.Split(dbFileSeparator)[0];
                    dataCheck.Number = Convert.ToInt32(fileName.Split(dbArchiveFileSeparator)[1]);
                    fileList.Add(dataCheck);
                }
            }

            return fileList;
        }
        private List<DataLogFileModel> GetFullFileList()
        {
            var fileList = new List<DataLogFileModel>();
            var checkFolder = Directory.Exists(Path.Combine(new string[] { dbPath, dbFolderPath }));
            if (checkFolder == false)
                return null;

            string[] files = Directory.GetFiles(Path.Combine(new string[] { dbPath, dbFolderPath }));
            foreach (var item in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(item);
                var dataCheck = new DataLogFileModel();
                dataCheck.FullPath = item;

                if (fileName == dbFileName)
                {
                    dataCheck.FileName = fileName;
                    dataCheck.Number = 2000000000;
                }
                else
                {

                    dataCheck.FileName = fileName.Split(dbFileSeparator)[0];
                    dataCheck.Number = Convert.ToInt32(fileName.Split(dbArchiveFileSeparator)[1]);
                }


                fileList.Add(dataCheck);
            }

            return fileList;
        }
    }
}
