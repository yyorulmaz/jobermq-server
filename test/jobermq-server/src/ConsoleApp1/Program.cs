using JoberMQ.Entities.Enums.Configuration;
using JoberMQ.Extensions;






#region senaryo 1
//var jober = JoberMQ.JoberHost
//.CreateDefaultBuilder()
//.Build();

//jober.StartAsync(); 
#endregion


#region senaryo 1
var configuration = JoberMQ.Factories.Configuration.ConfigurationFactory.CreateConfiguration(ConfigurationFactoryEnum.Default);

var jober = JoberMQ.JoberHost
.CreateDefaultBuilder()
.Configuration(configuration)
.Build();


jober.StartAsync();
#endregion














Console.ReadLine();
