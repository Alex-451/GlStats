using GlStats.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlStats.DataAccess.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _context;
    public TeamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<Team> GetAsync(int id)
    {
        return await _context.Teams.SingleAsync(t => t.Id == id);
    }

    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }

    public async Task UpdateAsync(Team team)
    {
        var entry = await _context.Teams.FindAsync(team.Id);

        if (entry != null)
        {
            entry = team;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entry = await _context.Teams.FindAsync(id);

        if (entry != null)
        {
            _context.Teams.Remove(entry);
        }
    }
}