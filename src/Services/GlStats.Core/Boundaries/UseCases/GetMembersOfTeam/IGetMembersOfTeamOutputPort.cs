using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;

public interface IGetMembersOfTeamOutputPort 
{
    void NoDatabaseConnection();
    void Default(IEnumerable<TeamMember> teamMembers);
}