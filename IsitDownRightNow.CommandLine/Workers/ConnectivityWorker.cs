using IsitDownRightNow.CommandLine.Services;
using IsitDownRightNow.CommandLine.Services.IpInfo;

namespace IsitDownRightNow.CommandLine.Workers;

public class ConnectivityWorker(
    IpInfoService ipInfoService,
    ConnectivityService connectivityService,
    ILogger<ConnectivityWorker> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting connectivity worker");
        
        await DoWork();
        
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWork();
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Stopping connectivity worker");
        }
    }
    
    private async Task DoWork()
    {
        logger.LogInformation("Connectivity check running at: {Time}", DateTimeOffset.Now);

        try
        {
            var ipInfo = await ipInfoService.GetIpInfo();
            if (ipInfo is null)
            {
                logger.LogError("IpInfo response is null");
                return;
            }

            logger.LogInformation("Connectivity check succeeded with IP: {IpInfo}", ipInfo.Ip);
        }
        catch (HttpRequestException exception)
        {
            logger.LogError("Connectivity check failed with exception: {Exception}", exception);

            try
            {
                logger.LogInformation("Attempting to reach controller");
                var time = await connectivityService.PingController();
                logger.LogInformation("Successfully reached controller with reply time: {ReplyTime}ms", time);
            }
            catch (Exception pingException)
            {
                logger.LogError("Connectivity to controller failed with exception: {Exception}", pingException);
            }
        }
    }
}