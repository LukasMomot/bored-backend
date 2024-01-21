namespace Bored.CloudFunctions.Services;

public class BoredApiService(IHttpClientFactory httpClientFactory) : IBoredApiService
{
    public async Task<string> GetActivity()
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://www.boredapi.com/api/activity");
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}