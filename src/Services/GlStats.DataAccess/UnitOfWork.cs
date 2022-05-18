using GlStats.DataAccess.Repositories;
using LiteDB;

namespace GlStats.DataAccess;

//todo fix unit of work pattern
public class UnitOfWork : IUnitOfWork
{
    public ITeamRepository TeamRepository { get; }
    public ITeamMemberRepository TeamMemberRepository { get; }

    private readonly LiteDatabase _database;

    public UnitOfWork(LiteDatabase database, ITeamRepository teamRepository, ITeamMemberRepository teamMemberRepository)
    {
        _database = database;
        _database.BeginTrans();

        TeamRepository = teamRepository;
        TeamMemberRepository = teamMemberRepository;

    }


    public void Commit(bool dryRun = false)
    {
        try
        {
            if (dryRun)
                return;

            _database.Commit();
        }
        catch
        {
            _database.Rollback();
            throw;
        }
        finally
        {
            //_database.Dispose();
            //_database.BeginTrans();
        }
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
            _database.Dispose();
        }
    }
}