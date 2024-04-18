using System.Net.NetworkInformation;
using IsitDownRightNow.CommandLine.Configuration;
using Microsoft.Extensions.Options;

namespace IsitDownRightNow.CommandLine.Services;

public class ConnectivityService(IOptions<NetworkOptions> options)
{
    public async Task<double> PingController()
    {
        var pinger = new Ping();
        var reply = await pinger.SendPingAsync(options.Value.ControllerIPAddress);
        return reply.RoundtripTime;
    }
}