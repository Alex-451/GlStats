using GlStats.DataAccess.Entities;

namespace GlStats.DataAccess.Repositories;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team> GetAsync(int id);
    Task<Team> AddAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(int id);
}