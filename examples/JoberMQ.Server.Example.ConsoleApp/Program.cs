#region senaryo 1
////var jober = JoberMQ.JoberHost
////.CreateDefaultBuilder()
////.Build();

////jober.StartAsync(); 
#endregion


#region senaryo 2
using JoberMQ;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Extensions;
using System;
using System.Collections.Generic;

var configuration = JoberHost.CreateConfiguration();

var jober = JoberMQ.JoberHost
.CreateDefaultBuilder()
.Configuration(configuration)
.Build();


jober.StartAsync();
#endregion

Console.WriteLine("ok");
Console.ReadLine();

