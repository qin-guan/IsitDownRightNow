using IsitDownRightNow.ServiceDefaults;
using IsitDownRightNow.WorkerService;
using IsitDownRightNow.WorkerService.Configuration;
using IsitDownRightNow.WorkerService.Services;
using IsitDownRightNow.WorkerService.Services.Reflector;
using IsitDownRightNow.WorkerService.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOptions<NetworkOptions>()
    .Bind(
        builder.Configuration.GetSection(nameof(NetworkOptions))
    )
    .Validate(options => !string.IsNullOrEmpty(options.ControllerIPAddress))
    .ValidateOnStart();

builder.Services.AddHttpClient<ReflectorService>()
    .AddStandardResilienceHandler();

builder.Services.AddSingleton<ConnectivityService>();
builder.Services.AddHostedService<ConnectivityWorker>();

var host = builder.Build();
host.Run();