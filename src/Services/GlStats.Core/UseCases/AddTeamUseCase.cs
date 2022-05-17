using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.AddTeam;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class AddTeamUseCase : IAddTeamUseCase
{
    private readonly IAddTeamOutputPort _output;
    private readonly ITeamsProvider _teamsProvider;

    public AddTeamUseCase(IAddTeamOutputPort output, ITeamsProvider teamsProvider)
    {
        _output = output;
        _teamsProvider = teamsProvider;
    }

    public async Task Execute(string teamName)
    {
        try
        {
            var team = await _teamsProvider.AddTeamAsync(teamName);
            _output.Default(team);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}