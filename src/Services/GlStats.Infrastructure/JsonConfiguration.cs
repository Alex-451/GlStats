using System.Text.Json;
using GlStats.Core.Entities;

namespace GlStats.Infrastructure;

public class JsonConfiguration
{
    private readonly string _configFullPath = string.Empty;
    private readonly string _configFolder = string.Empty;

    public JsonConfiguration()
    {
        var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(dataFolder, "gl-stats");
        Directory.CreateDirectory(appFolder);
        _configFullPath = Path.Combine(appFolder, "config.json");
        _configFolder = appFolder;
    }

    public Config GetConfig()
    {
        try
        {
            var json = File.ReadAllText(_configFullPath);
            return JsonSerializer.Deserialize<Config>(json);
        }
        catch (FileNotFoundException)
        {
            return InitializeDefaultConfig();
        }
    }

    private Config InitializeDefaultConfig()
    {
        var config = new Config(connectionString: $@"{_configFolder}\gl-stats.db");
        StoreConfig(config);
        return config;
    }

    public void StoreConfig(Config config)
    {
        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions{ WriteIndented = true });
        File.WriteAllText(_configFullPath, json);
    }
};


