using Azure.Identity;
using BoredBackend;
using BoredBackend.Data;
using BoredBackend.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Use dotenv for local development

//DotEnv.Load();
//builder.Configuration.AddEnvironmentVariables();

var kvUri = builder.Configuration["AZUKE_KEY_VAULT_ENDPOINT"];
if (kvUri != null)
{
    builder.Configuration.AddAzureKeyVault(new Uri(kvUri), new DefaultAzureCredential());
}

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
app.MapGet("/secret", (IConfiguration config) => config["firstSecret"]);
app.MapActivityEndpoints();

app.Run();
