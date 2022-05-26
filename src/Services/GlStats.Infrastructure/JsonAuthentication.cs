using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Core.Entities;

namespace GlStats.Infrastructure;

public class JsonAuthentication : IAuthentication
{
    private readonly JsonConfiguration _config;

    public JsonAuthentication(JsonConfiguration config)
    {
        _config = config;
    }

    public Config GetConfig()
    {
        return _config.GetConfig();
    }

    public void SetConfig(string url, string apiKey)
    {
        var config = _config.GetConfig();
        config.GitLabUrl = url;
        config.GitLabToken = apiKey;
        _config.StoreConfig(config);
    }

    public void UpdateSettings(string cultureName)
    {
        var config = _config.GetConfig();
        config.CurrentCulture = cultureName;
        _config.StoreConfig(config);
    }
}