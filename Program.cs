using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
// get value from configuration
var value = builder.Configuration["key"];
// display value
Console.WriteLine($"Value: {value}");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
