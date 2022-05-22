using GlStats.DataAccess.Entities;
using LiteDB;

namespace GlStats.DataAccess.Repositories.Implementations;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly LiteDatabase _database;

    private readonly ILiteCollection<TeamMember> _col;

    public TeamMemberRepository(LiteDatabase database)
    {
        _database = database;

        _col = _database.GetCollection<TeamMember>(nameof(TeamMember), BsonAutoId.Int32);
    }

    public IEnumerable<TeamMember> GetMembersOfTeam(int teamId)
    {
        var result = _col.Find(x => x.TeamId == teamId);
        return result;
    }

    public void RemoveFromTeam(int teamId, string gitLabUserId)
    {
        _col.DeleteMany(x => x.TeamId == teamId && x.MemberId == gitLabUserId);
    }

    public int AddToTeam(int teamId, string gitLabUserId)
    {
        var id = _col.Insert(new TeamMember
        {
            TeamId = teamId,
            MemberId = gitLabUserId,
        });

        return id.AsInt32;
    }
}