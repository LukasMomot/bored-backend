using System.Net.Http.Json;
using Bored.Models;

namespace Bored.Services.ExternalClients;

public class BoredApiService(HttpClient httpClient) : IBoredApiService
{
    public async Task<ActivityStaging?> GetActivity()
    {
        var response = await httpClient.GetAsync("https://www.boredapi.com/api/activity");
        var content = await response.Content.ReadFromJsonAsync<ActivityStaging>();
        return content;
    }
}