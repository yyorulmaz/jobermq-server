using JoberMQ.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DfConfiguration : IConfiguration
    {
        public DfConfiguration()
        {
            configurationDatabase = JoberMQ.Database.Factories.ConfigurationDatabaseFactory.CreateConfigurationData(DefaultDatabaseConst.ConfigurationDatabaseFactory);
        }

        IConfigurationDatabase configurationDatabase;
        public IConfigurationDatabase ConfigurationDatabase { get => configurationDatabase; set => configurationDatabase = value; }
    }
}
