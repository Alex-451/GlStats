using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.UpdateTeam;

public interface IUpdateTeamUseCase
{
    void Execute(int teamId, Team team);
}