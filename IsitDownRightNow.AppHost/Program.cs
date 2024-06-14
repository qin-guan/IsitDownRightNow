using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<IsitDownRightNow_WorkerService>("worker");

builder.Build().Run();