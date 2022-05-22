using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.GetTeamById;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class GetTeamByIdUseCase : IGetTeamByIdUseCase
{
    private readonly IGetTeamByIdOutputPort _output;
    private readonly ITeamsProvider _teamsProvider;

    public GetTeamByIdUseCase(IGetTeamByIdOutputPort output, ITeamsProvider teamsProvider)
    {
        _output = output;
        _teamsProvider = teamsProvider;
    }

    public void Execute(int teamId)
    {
        try
        {
            var team = _teamsProvider.GetTeamById(teamId);
            _output.Default(team);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}