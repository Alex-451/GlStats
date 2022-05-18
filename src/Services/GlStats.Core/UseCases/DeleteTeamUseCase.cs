using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.DeleteTeam;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class DeleteTeamUseCase : IDeleteTeamUseCase
{
    private readonly IDeleteTeamOutputPort _output;
    private readonly ITeamsProvider _teamsProvider;

    public DeleteTeamUseCase(IDeleteTeamOutputPort output, ITeamsProvider teamsProvider)
    {
        _output = output;
        _teamsProvider = teamsProvider;
    }

    public void Execute(int teamId)
    {
        try
        {
            var isDeleted = _teamsProvider.DeleteTeam(teamId);
            _output.Default(isDeleted);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}