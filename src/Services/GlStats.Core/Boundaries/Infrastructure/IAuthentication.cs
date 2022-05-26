using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Infrastructure;

public interface IAuthentication
{
    Config GetConfig();
    void SetConfig(string url, string apiKey);
    public void UpdateSettings(string cultureName);
}