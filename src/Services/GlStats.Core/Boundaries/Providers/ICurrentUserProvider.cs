using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Providers;

public interface ICurrentUserProvider
{
    Task<CurrentUser> GetCurrentUserAsync();
}