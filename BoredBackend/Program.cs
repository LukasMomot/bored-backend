using Bored.Models.Options;
using BoredBackend.Data;
using BoredBackend.Endpoints;
using BoredBackend.Utils;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppConfiguration();

var appInsightsKey = builder.Configuration["APP_INSIGHTS_KEY"];
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = appInsightsKey;
});

var connectionString = builder.Configuration["AzureDb"];
builder.Services.AddDbContext<BoredDbContext>(options =>
{
    options.UseSqlServer(connectionString, optionsBuilder =>
    {
        optionsBuilder.EnableRetryOnFailure(5);
    });
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, name: "AzureSQL", tags: new[] { "db", "sql", "azure" });

var app = builder.Build();

// Map health checks endpoint to use the UIResponseWriter
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true, // Include all registered health checks
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse // Use the UIResponseWriter
});

app.MapGet("/", (ILogger<Program> logger) => {
    logger.LogInformation("Hello from the BoredBackend!");
    return "Hello from the BoredBackend!";
});

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
