using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.Providers;

public interface ITeamMembersProvider
{
    IEnumerable<TeamMember> GetMembersOfTeam(int teamId);

    int AddMemberToTeam(int teamId, string userId);

}