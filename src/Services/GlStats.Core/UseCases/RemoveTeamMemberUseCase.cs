using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.RemoveTeamMember;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class RemoveTeamMemberUseCase : IRemoveTeamMemberUseCase
{
    private readonly IRemoveTeamMemberOutputPort _output;
    private readonly ITeamMembersProvider _membersProvider;

    public RemoveTeamMemberUseCase(IRemoveTeamMemberOutputPort output, ITeamMembersProvider membersProvider)
    {
        _output = output;
        _membersProvider = membersProvider;
    }

    public void Execute(int teamId, string userId)
    {
        try
        {
            var rowsDeleted = _membersProvider.RemoveMemberFromTeam(teamId, userId);
            if(rowsDeleted == 1)
                _output.Default(true);
        }
        catch (NoDatabaseConnection)
        {
            _output.NoDatabaseConnection();
        }
    }
}