using BoredBackend.Data;
using BoredBackend.Endpoints;
using BoredBackend.Messaging;
using BoredBackend.Utils;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.AddAppConfiguration();

var connectionString = builder.Configuration["AzureDb"];
builder.Services.AddDbContext<BoredDbContext>(options =>
{
    options.UseSqlServer(connectionString, optionsBuilder =>
    {
        optionsBuilder.EnableRetryOnFailure(5);
    });
});

var serviceBusCs = builder.Configuration["ServiceBus"];
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<StartFakeStagingConsumer>();
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(serviceBusCs);
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/secret", (IConfiguration config) => config["firstSecret"]);
app.MapActivityEndpoints();

app.Run();
