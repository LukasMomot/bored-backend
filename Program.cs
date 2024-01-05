using BoredBackend;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();

var t = builder.Configuration["AzureDb_ConnectionString"];
Console.WriteLine($"key2: {t}");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
