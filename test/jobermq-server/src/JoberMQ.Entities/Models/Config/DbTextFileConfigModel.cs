using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class DbTextFileConfigModel
    {
        public string DbPath { get; set; }
        public string DbFolderPath { get; set; }
        public string DbFileName { get; set; }
        public char DbFileSeparator { get; set; }
        public char DbArchiveFileSeparator { get; set; }
        public string DbFileExtension { get; set; }
        public int MaxRowCount { get; set; }
    }
}
