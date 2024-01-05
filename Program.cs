using BoredBackend;
using BoredBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<BoredDbContext>(options =>
{
    var connectionString = builder.Configuration["AzureDb_ConnectionString"];
    options.UseSqlServer(connectionString, optionsBuilder =>
    {
        optionsBuilder.EnableRetryOnFailure(2);
    });
});

var connection = builder.Configuration["AzureDb_ConnectionString"];
Console.WriteLine($"Connection: {connection}");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
