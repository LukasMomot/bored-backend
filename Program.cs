using BoredBackend;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();

var t = builder.Configuration["test2"];
Console.WriteLine($"key2: {t}");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
