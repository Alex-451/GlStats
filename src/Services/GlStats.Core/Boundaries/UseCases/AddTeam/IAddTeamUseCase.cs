using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.AddTeam;

public interface IAddTeamUseCase
{
    Task Execute(string teamName);
}