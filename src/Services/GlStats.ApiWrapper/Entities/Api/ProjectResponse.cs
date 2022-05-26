namespace GlStats.ApiWrapper.Entities.Api;

/// <summary>
///     Project
/// </summary>
public class ProjectResponse
{
    /// <summary>
    ///     Id of project
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     Name of project
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Avatar url of project
    /// </summary>
    public Uri? AvatarUrl { get; set; }

    /// <summary>
    ///     Description of project
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Url of project
    /// </summary>
    public Uri? WebUrl { get; set; }
}