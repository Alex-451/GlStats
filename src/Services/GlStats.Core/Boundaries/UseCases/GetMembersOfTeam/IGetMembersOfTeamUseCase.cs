namespace GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;

public interface IGetMembersOfTeamUseCase
{
    void Execute(int teamId);
}