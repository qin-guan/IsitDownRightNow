namespace IsitDownRightNow.WorkerService.Workers;

public class OktaToDoorAccessSyncWorker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}