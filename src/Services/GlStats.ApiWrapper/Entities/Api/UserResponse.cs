namespace GlStats.ApiWrapper.Entities.Api;

/// <summary>
///     User
/// </summary>
public class UserResponse
{
    /// <summary>
    ///     Id of the user
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     Avatar of the user
    /// </summary>
    public Uri AvatarUrl { get; set; }

    /// <summary>
    ///     Username of the user
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    ///     Name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Public email of the user
    /// </summary>
    public string PublicEmail { get; set; } = string.Empty;
}