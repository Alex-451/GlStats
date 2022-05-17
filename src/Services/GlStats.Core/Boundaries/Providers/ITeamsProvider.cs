using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Providers;

public interface ITeamsProvider
{
    Task<IEnumerable<Team>> GetTeamsAsync();
    Task<Team> AddTeamAsync(string name);
}