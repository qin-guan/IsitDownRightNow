using IsitDownRightNow.WorkerService.Extensions;
using IsitDownRightNow.WorkerService.Services;
using IsitDownRightNow.WorkerService.Services.Reflector;

namespace IsitDownRightNow.WorkerService.Workers;

public class ConnectivityWorker(
    ReflectorService reflectorService,
    ConnectivityService connectivityService,
    ILogger<ConnectivityWorker> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.StartWorker();

        await DoWork();

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWork();
            }
        }
        catch (OperationCanceledException)
        {
            logger.StopWorker();
        }
    }

    private async Task DoWork()
    {
        logger.StartConnectivityCheck();

        try
        {
            var ip = await reflectorService.GetIPAddress();
            logger.ConnectivityCheckSuccess(ip);
        }
        catch (HttpRequestException exception)
        {
            logger.ConnectivityCheckFail(exception);

            try
            {
                logger.StartControllerCheck();
                var time = await connectivityService.PingController();
                logger.ControllerCheckSuccess(time);
            }
            catch (Exception pingException)
            {
                logger.ControllerCheckFail(pingException);
            }
        }
    }
}