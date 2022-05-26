using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Providers;

public interface IGitLabProvider
{
    Task<CurrentUser> GetCurrentUserAsync();
    Task<IEnumerable<User>> SearchUsersAsync(string search);
    Task<IEnumerable<User>> GetUsersByIdsAsync(string[] ids);
    Task<IEnumerable<Project>> GetProjectsAsync(ProjectSearchOptions options);
}