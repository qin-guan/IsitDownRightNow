using System.Net.NetworkInformation;
using IsitDownRightNow.WorkerService.Configuration;
using Microsoft.Extensions.Options;

namespace IsitDownRightNow.WorkerService.Services;

public class ConnectivityService(IOptions<NetworkOptions> options)
{
    public async Task<double> PingController()
    {
        var pinger = new Ping();
        var reply = await pinger.SendPingAsync(options.Value.ControllerIPAddress);
        return reply.RoundtripTime;
    }
}