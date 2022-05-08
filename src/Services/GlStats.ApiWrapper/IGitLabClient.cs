using GlStats.ApiWrapper.Entities.Api;

namespace GlStats.ApiWrapper;

public interface IGitLabClient
{
    Task<CurrentUserResponse> GetCurrentUserAsync();
    bool IsAuthenticated();
}