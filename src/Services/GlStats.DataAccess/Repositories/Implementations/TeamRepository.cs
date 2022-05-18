using GlStats.DataAccess.Entities;
using LiteDB;

namespace GlStats.DataAccess.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    private readonly LiteDatabase _database;

    private readonly ILiteCollection<Team> _col;

    public TeamRepository(LiteDatabase database)
    {
        _database = database;

        _col = _database.GetCollection<Team>(nameof(Team), BsonAutoId.Int32);
    }

    public IEnumerable<Team> GetAll()
    {
        var result = _col.FindAll();
        return result;
    }

    public Team GetById(int id)
    {
        return _col.FindById(id);
    }

    public int Add(Team team)
    {
        team.CreationDate = DateTime.Now;

        var id = _col.Insert(team);
        return id.AsInt32;
    }

    public bool Update(int id, Team team)
    {
        return _col.Update(id, team);
    }

    public bool Delete(int id)
    {
        return _col.Delete(id);
    }
}