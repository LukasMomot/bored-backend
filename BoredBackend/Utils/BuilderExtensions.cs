

using Azure.Identity;

namespace BoredBackend.Utils;

public static class BuilderExtensions
{
    public static void AddAppConfiguration(this WebApplicationBuilder builder)
    {
        // Use dotenv for local development
        if (builder.Environment.IsDevelopment())
        {
            DotEnv.Load();
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
    }
}
