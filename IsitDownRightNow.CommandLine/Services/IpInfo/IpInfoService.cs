using System.Net.Http.Json;

namespace IsitDownRightNow.CommandLine.Services.IpInfo;

public class IpInfoService
{
    private readonly HttpClient _httpClient;

    public IpInfoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://ipinfo.io");
    }

    public Task<IpInfoResponse?> GetIpInfo() => _httpClient.GetFromJsonAsync("/", JsonContext.Default.IpInfoResponse);
}