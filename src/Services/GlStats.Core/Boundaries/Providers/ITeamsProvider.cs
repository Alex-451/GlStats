using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Providers;

public interface ITeamsProvider
{
    IEnumerable<Team> GetTeams();

    Team GetTeamById(int id);

    int AddTeam(Team team);

    bool UpdateTeam(int id, Team team);

    bool DeleteTeam(int teamId);
}