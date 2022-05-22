namespace GlStats.Core.Boundaries.UseCases.AddMemberToTeam;

public interface IAddMemberToTeamUseCase
{
    void Execute(int teamId, string userId);

}