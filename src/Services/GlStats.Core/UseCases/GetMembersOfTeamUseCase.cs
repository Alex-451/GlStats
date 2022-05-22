using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.GetMembersOfTeam;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class GetMembersOfTeamUseCase : IGetMembersOfTeamUseCase
{
    private readonly IGetMembersOfTeamOutputPort _output;
    private readonly ITeamMembersProvider _teamMembersProvider;

    public GetMembersOfTeamUseCase(IGetMembersOfTeamOutputPort output, ITeamMembersProvider teamMembersProvider)
    {
        _output = output;
        _teamMembersProvider = teamMembersProvider;
    }

    public void Execute(int teamId)
    {
        try
        {
            var teamMembers = _teamMembersProvider.GetMembersOfTeam(teamId);
            _output.Default(teamMembers);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}