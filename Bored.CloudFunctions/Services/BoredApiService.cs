namespace Bored.CloudFunctions.Services;

public class BoredApiService(HttpClient httpClient) : IBoredApiService
{
    public async Task<string> GetActivity()
    {
        var response = await httpClient.GetAsync("https://www.boredapi.com/api/activity");
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}