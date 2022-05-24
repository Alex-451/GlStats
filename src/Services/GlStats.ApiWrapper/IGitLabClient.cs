using GlStats.ApiWrapper.Entities.Api;

namespace GlStats.ApiWrapper;

public interface IGitLabClient
{
    Task<CurrentUserResponse> GetCurrentUserAsync();

    Task<IEnumerable<UserResponse>> SearchUsersAsync(string search);

    Task<UserResponse> GetUserByIdAsync(string id);

    Task<IEnumerable<UserResponse>> GetUsersByIdAsync(string[] ids);

    bool IsAuthenticated();
}