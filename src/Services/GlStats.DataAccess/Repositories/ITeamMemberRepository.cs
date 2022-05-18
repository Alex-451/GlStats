namespace GlStats.DataAccess.Repositories;

public interface ITeamMemberRepository
{
    int AddToTeam(int teamId, string gitLabUserId);
    void RemoveFromTeam(int teamId, string gitLabUserId);
}