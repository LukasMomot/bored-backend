using BoredBackend.Data;
using BoredBackend.Endpoints;
using BoredBackend.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppConfiguration();

var connectionString = builder.Configuration["AzureDb"];
builder.Services.AddDbContext<BoredDbContext>(options =>
{
    options.UseSqlServer(connectionString, optionsBuilder =>
    {
        optionsBuilder.EnableRetryOnFailure(5);
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/secret", (IConfiguration config) =>
{
    var secret = config["firstSecret"]; // This is cooming from keyvalut
    var azureAppConfig = config["azureSampleConfig"];
    
    // return anonymous object with keys and values Secret, AzureAppConfig
    return new { Secret = secret, AzureAppConfig = azureAppConfig };
    
});
app.MapActivityEndpoints();

app.Run();
