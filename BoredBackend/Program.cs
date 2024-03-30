using Bored.Models.Options;
using BoredBackend.Data;
using BoredBackend.Endpoints;
using BoredBackend.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
app.MapGet("/secret", (IConfiguration config, IOptions<TestOptions> options) =>
{
    var secret = config["firstSecret"]; // This is cooming from keyvalut
    var azureAppConfig = config["azureSampleConfig"];
    var testOptions = options.Value;
    
    // return anonymous object with keys and values Secret, AzureAppConfig, TestOptions
    return new { Secret = secret, AzureAppConfig = azureAppConfig, TestOptions = testOptions };
    
});
app.MapActivityEndpoints();

app.Run();
