using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Database;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Factories.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Implementation.Jober.Default
{
    public class DfJober : IJober
    {
        public DfJober(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        #region Configuration
        IConfiguration configuration;
        public IConfiguration Configuration { get => configuration; set => configuration = value; }
        #endregion

        #region DatabaseService
        IDatabaseService databaseService;
        IDatabaseService IJober.DatabaseService { get => databaseService; set => databaseService = value; }
        #endregion



        public async Task StartAsync()
        {
            databaseService = DatabaseServiceFactory.CreateDatabaseService(configuration.ConfigurationDatabase);
            //databaseService.CreateDatabases();
            //databaseService.Setups();

            throw new System.NotImplementedException();
        }
    }
}
