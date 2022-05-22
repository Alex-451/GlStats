using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;
using GlStats.DataAccess;

namespace GlStats.Infrastructure.Providers;

public class TeamMembersProvider : ITeamMembersProvider
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamMembersProvider(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IEnumerable<TeamMember> GetMembersOfTeam(int teamId)
    {
        try
        {
            var id = _unitOfWork.TeamMemberRepository.GetMembersOfTeam(teamId).Select(ToTeamMember);
            _unitOfWork.Commit();
            return id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NoDatabaseConnection();
        }
    }

    public int AddMemberToTeam(int teamId, string userId)
    {
        try
        {
            return _unitOfWork.TeamMemberRepository.AddToTeam(teamId, userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NoDatabaseConnection();
        }
    }

    private TeamMember ToTeamMember(DataAccess.Entities.TeamMember response)
    {
        return new TeamMember
        {
            Id = response.Id,
            TeamId = response.TeamId,
            MemberId = response.MemberId,
        };
    }
}