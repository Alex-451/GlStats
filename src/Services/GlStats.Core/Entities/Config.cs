namespace GlStats.Core.Entities;

public class Config
{
    public Config(string gitLabUrl = "", string gitLabToken = "", string connectionString = "")
    {
        GitLabUrl = gitLabUrl;
        GitLabToken = gitLabToken;
        ConnectionString = connectionString;
    }
    public string GitLabUrl { get; set; } = string.Empty;
    public string GitLabToken { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string CurrentCulture { get; set; } = "en-UK";
}