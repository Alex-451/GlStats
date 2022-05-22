namespace GlStats.Core.Boundaries.UseCases.AddMemberToTeam;

public interface IAddMemberToTeamOutputPort
{
    void NoDatabaseConnection();
    void Default(int teamMemberId);
}