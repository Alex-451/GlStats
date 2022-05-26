using GlStats.ApiWrapper.Entities.Api;
using GlStats.ApiWrapper.Entities.Requests;

namespace GlStats.ApiWrapper;

public interface IGitLabClient
{
    Task<CurrentUserResponse> GetCurrentUserAsync();

    Task<IEnumerable<UserResponse>> SearchUsersAsync(string search);

    Task<UserResponse> GetUserByIdAsync(string id);

    Task<IEnumerable<UserResponse>> GetUsersByIdAsync(string[] ids);

    Task<IEnumerable<ProjectResponse>> GetProjectsAsync(ProjectQueryOptions options);

    bool IsAuthenticated();
}