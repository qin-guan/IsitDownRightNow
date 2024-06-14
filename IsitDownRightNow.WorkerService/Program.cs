using IsitDownRightNow.ServiceDefaults;
using IsitDownRightNow.WorkerService;
using IsitDownRightNow.WorkerService.Configuration;
using IsitDownRightNow.WorkerService.Services;
using IsitDownRightNow.WorkerService.Services.Reflector;
using IsitDownRightNow.WorkerService.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<NetworkOptions>(
    builder.Configuration.GetSection(nameof(NetworkOptions))
);

builder.Services.AddHttpClient<ReflectorService>();
builder.Services.AddSingleton<ConnectivityService>();
builder.Services.AddHostedService<ConnectivityWorker>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();