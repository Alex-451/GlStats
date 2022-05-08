namespace GlStats.ApiWrapper.Entities.Api;

/// <summary>
///     Current user
/// </summary>
public class CurrentUserResponse
{
    /// <summary>
    ///     Id of the current user
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     Name of the current user
    /// </summary>
    public string Name { get; set; } = string.Empty;
}