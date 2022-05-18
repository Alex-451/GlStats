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

    public IEnumerable<Team> GetTeams()
    {
        try
        {
            return _unitOfWork.TeamRepository.GetAll().Select(ToTeam);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NoDatabaseConnection();
        }
    }

    public int AddTeam(Team team)
    {
        try
        {
            var id= _unitOfWork.TeamRepository.Add(new DataAccess.Entities.Team
            {
                Name = team.Name,
            });
            _unitOfWork.Commit();
            return id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NoDatabaseConnection();
        }
    }

    public bool UpdateTeam(int id, Team team)
    {
        try
        {
            var hasUpdated = _unitOfWork.TeamRepository.Update(id, new DataAccess.Entities.Team
            {
                Name = team.Name,
            });
            _unitOfWork.Commit();
            return hasUpdated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool DeleteTeam(int teamId)
    {
        try
        {
            var deleted = _unitOfWork.TeamRepository.Delete(teamId);
            _unitOfWork.Commit();
            return deleted;
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