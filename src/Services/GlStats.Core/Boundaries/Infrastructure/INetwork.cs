namespace GlStats.Core.Boundaries.Infrastructure;

public interface INetwork
{
    Task<string> PostAsync(string url, string apiKey, string body);
}