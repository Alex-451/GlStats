using GlStats.DataAccess.Entities;

namespace GlStats.DataAccess.Repositories;

public interface ITeamMemberRepository
{
    IEnumerable<TeamMember> GetMembersOfTeam(int teamId);
    int AddToTeam(int teamId, string gitLabUserId);
    int RemoveFromTeam(int teamId, string gitLabUserId);
}