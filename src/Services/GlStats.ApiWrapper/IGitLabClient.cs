using GlStats.ApiWrapper.Entities.Api;

namespace GlStats.ApiWrapper;

public interface IGitLabClient
{
    Task<CurrentUserResponse> GetCurrentUserAsync();
    Task<IEnumerable<UserResponse>> SearchUsersAsync(string search);
    bool IsAuthenticated();
}