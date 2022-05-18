using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Providers;

public interface ITeamsProvider
{
    IEnumerable<Team> GetTeams();

    int AddTeam(Team team);

    bool UpdateTeam(int id, Team team);

    bool DeleteTeam(int teamId);
}