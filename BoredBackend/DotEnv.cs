namespace BoredBackend;

[Obsolete("This class is obsolete. Please use dotenv.net.DotEnv.Load() instead.")]
public static class DotEnv
{
    public static void Load()
    {
        var envFile = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        if (!File.Exists(envFile))
        {
            return;
        }

        var lines = File.ReadAllLines(envFile);
        foreach (var line in lines)
        {
            var parts = line.Split("=\"", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                continue;
            }

            var key = parts[0].Trim();
            var value = parts[1].Trim('"').Trim();
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}