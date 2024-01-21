using Bored.CloudFunctions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // TODO: Switch to typed http client
        services.AddTransient<IBoredApiService, BoredApiService>();
        services.AddHttpClient();
    })
    .Build();

host.Run();