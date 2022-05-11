using GlStats.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GlStats.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public ITeamRepository TeamRepository { get; }

    public UnitOfWork(ApplicationDbContext context, ITeamRepository teamRepository)
    {
        _context = context;
        _context.Database.Migrate();
        TeamRepository = teamRepository;
    }

    public void Commit(bool dryRun = false)
    {
        if (dryRun)
            return;

        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}