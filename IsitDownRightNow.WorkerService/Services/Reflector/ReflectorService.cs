using System.Net.Http.Json;

namespace IsitDownRightNow.WorkerService.Services.Reflector;

public class ReflectorService
{
    private readonly HttpClient _httpClient;

    public ReflectorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://reflector.qinguan.me");
    }

    public Task<string> GetIPAddress() => _httpClient.GetStringAsync("/");
}