using Azure.Identity;
using Bored.Models.Options;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace BoredBackend.Utils;

public static class BuilderExtensions
{
    public static void AddAppConfiguration(this WebApplicationBuilder builder)
    {
        // Use dotenv for local development
        if (builder.Environment.IsDevelopment())
        {
            dotenv.net.DotEnv.Load();
            builder.Configuration.AddEnvironmentVariables();
        }
        else
        {
            var kvUri = builder.Configuration["AZUKE_KEY_VAULT_ENDPOINT"];
            if (kvUri != null)
            {
                builder.Configuration.AddAzureKeyVault(new Uri(kvUri), new DefaultAzureCredential());
            }
        }
        
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            var config = Environment.GetEnvironmentVariable("AZURE_APP_CONFIG");
            options
                .Connect(config)
                .ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                })
                .Select(KeyFilter.Any, LabelFilter.Null)
                .Select(KeyFilter.Any, "Development");
        });

        // Alternative way to bind options
        // builder.Services
        //     .AddOptions<TestOptions>()
        //     .Bind(builder.Configuration.GetSection(TestOptions.SectionName))
        //     .ValidateDataAnnotations()
        //     .ValidateOnStart();
        // builder.Configuration.GetValue<bool>()
        builder.Services.Configure<TestOptions>(builder.Configuration.GetSection(TestOptions.SectionName));
    }
}
