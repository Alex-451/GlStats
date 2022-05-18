using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.AddTeam;

public interface IAddTeamUseCase
{
    void Execute(Team team);
}