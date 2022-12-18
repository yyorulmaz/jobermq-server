using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.DbOpr;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class DbTextConfigModel
    {
        internal DbTextFactoryEnum DbTextFactory => ServerConst.DbOpr.DbTextFactory;
        internal ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas = ServerConst.DbOpr.DbTextFileConfigDatas;
    }
}
