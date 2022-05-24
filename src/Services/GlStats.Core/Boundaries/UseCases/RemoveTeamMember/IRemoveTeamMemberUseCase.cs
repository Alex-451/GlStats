namespace GlStats.Core.Boundaries.UseCases.RemoveTeamMember;

public interface IRemoveTeamMemberUseCase
{
    void Execute(int teamId, string userId);
}