namespace IsitDownRightNow.WorkerService.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Starting connectivity worker")]
    public static partial void StartWorker(this ILogger logger);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "Stopping connectivity worker")]
    public static partial void StopWorker(this ILogger logger);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "Starting new connectivity check")]
    public static partial void StartConnectivityCheck(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Connectivity check successful with IP {ip}")]
    public static partial void ConnectivityCheckSuccess(this ILogger logger, string ip);

    [LoggerMessage(Level = LogLevel.Error, Message = "Connectivity check failed")]
    public static partial void ConnectivityCheckFail(this ILogger logger, Exception exception);

    [LoggerMessage(Level = LogLevel.Information, Message = "Starting controller check")]
    public static partial void StartControllerCheck(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "Controller check successful with {latency}ms latency")]
    public static partial void ControllerCheckSuccess(this ILogger logger, double latency);

    [LoggerMessage(Level = LogLevel.Error, Message = "Controller check failed")]
    public static partial void ControllerCheckFail(this ILogger logger, Exception exception);
}