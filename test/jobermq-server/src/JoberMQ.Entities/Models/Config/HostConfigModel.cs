using System;
using System.Collections.Generic;
using System.Text;
using JoberMQ.Entities.Constants;

namespace JoberMQ.Entities.Models.Config
{
    public class HostConfigModel
    {
        internal string HostName => ServerConst.Hosting.HostName;
        public int Port => ServerConst.Hosting.Port;
        public int PortSsl => ServerConst.Hosting.PortSsl;
    }
}
