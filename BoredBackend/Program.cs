using BoredBackend;
using BoredBackend.Data;
using BoredBackend.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<BoredDbContext>(options =>
{
    var connectionString = builder.Configuration["AzureDb_ConnectionString"];
    options.UseSqlServer(connectionString, optionsBuilder =>
    {
        optionsBuilder.EnableRetryOnFailure(3);
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapActivityEndpoints();

app.Run();
