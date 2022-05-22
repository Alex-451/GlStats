namespace GlStats.Core.Entities;

public class User
{
    public string Id { get; set; } = string.Empty;

    public Uri AvatarUrl { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string PublicEmail { get; set; } = string.Empty;
}