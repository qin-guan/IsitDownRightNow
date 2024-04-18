using System.Text.Json.Serialization;
using IsitDownRightNow.CommandLine.Configuration;
using IsitDownRightNow.CommandLine.Services;
using IsitDownRightNow.CommandLine.Services.IpInfo;
using IsitDownRightNow.CommandLine.Workers;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog(options =>
    options
        .WriteTo.Console()
        .WriteTo.File("service.log", rollOnFileSizeLimit: true)
        .WriteTo.DatadogLogs(Environment.GetEnvironmentVariable("DD_API_KEY"), service: "isitdownrightnow")
);

builder.Services.Configure<NetworkOptions>(
    builder.Configuration.GetSection(nameof(NetworkOptions))
);

builder.Services.AddHttpClient<IpInfoService>();
builder.Services.AddSingleton<ConnectivityService>();
builder.Services.AddHostedService<ConnectivityWorker>();

var host = builder.Build();
host.Run();

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(IpInfoResponse))]
internal partial class JsonContext : JsonSerializerContext;