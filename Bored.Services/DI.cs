using Bored.CloudFunctions.Services;
using Bored.Services.ExternalClients;
using Microsoft.Extensions.DependencyInjection;

namespace Bored.Services;

public static class DI
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddHttpClient<IBoredApiService, BoredApiService>(client =>
        {
            client.BaseAddress = new Uri("https://www.boredapi.com/api/");
        });
        return services;
    }
}