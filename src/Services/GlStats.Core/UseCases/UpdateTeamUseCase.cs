using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.UpdateTeam;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class UpdateTeamUseCase : IUpdateTeamUseCase
{
    private readonly IUpdateTeamOutputPort _output;
    private readonly ITeamsProvider _teamsProvider;

    public UpdateTeamUseCase(IUpdateTeamOutputPort output, ITeamsProvider teamsProvider)
    {
        _output = output;
        _teamsProvider = teamsProvider;
    }

    public void Execute(int teamId, Team team)
    {
        try
        {
            var isUpdated = _teamsProvider.UpdateTeam(teamId, team);
            _output.Default(isUpdated);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}