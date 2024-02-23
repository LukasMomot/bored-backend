using Bored.CloudFunctions.Services;
using BoredBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IBoredApiService, BoredApiService>();
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        
        services.AddDbContext<BoredDbContext>(options =>
        {
            options.UseSqlServer(context.Configuration.GetConnectionString("AzureDb"), optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure(5);
            });
        });
    })
    .Build();

host.Run();