using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;
using GlStats.DataAccess;

namespace GlStats.Infrastructure.Providers;

public class TeamsProvider : ITeamsProvider
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamsProvider(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        try
        {
            return (await _unitOfWork.TeamRepository.GetAllAsync()).Select(ToTeam);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NoDatabaseConnection();
        }
    }

    public async Task<Team> AddTeamAsync(string name)
    {
        try
        {
            var addedTeam = await _unitOfWork.TeamRepository.AddAsync(new DataAccess.Entities.Team { Name = name, });
            _unitOfWork.Commit();
            return ToTeam(addedTeam);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NoDatabaseConnection();
        }
    }

    private Team ToTeam(DataAccess.Entities.Team response)
    {
        return new Team
        {
            Id = response.Id,
            Name = response.Name,
            CreationDate = response.CreationDate
        };
    }
}