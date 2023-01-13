using JoberMQ.Library.Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationMessage
    {
        public MemFactoryEnum MessageMasterFactory { get; set; }
        public MemDataFactoryEnum MessageMasterDataFactory { get; set; }
    }
}
