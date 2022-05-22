using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.AddMemberToTeam;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class AddMemberToTeamUseCase : IAddMemberToTeamUseCase
{
    private readonly IAddMemberToTeamOutputPort _output;
    private readonly ITeamMembersProvider _membersProvider;

    public AddMemberToTeamUseCase(IAddMemberToTeamOutputPort output, ITeamMembersProvider membersProvider)
    {
        _output = output;
        _membersProvider = membersProvider;
    }

    public void Execute(int teamId, string userId)
    {
        try
        {
            var id = _membersProvider.AddMemberToTeam(teamId, userId);
            _output.Default(id);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}