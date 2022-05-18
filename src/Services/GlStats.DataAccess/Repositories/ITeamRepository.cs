using GlStats.DataAccess.Entities;

namespace GlStats.DataAccess.Repositories;

public interface ITeamRepository
{
    IEnumerable<Team> GetAll();
    Team GetById(int id);
    int Add(Team team);
    bool Update(int id, Team team);
    bool Delete(int id);
}