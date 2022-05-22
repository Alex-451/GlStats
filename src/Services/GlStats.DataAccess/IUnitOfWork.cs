using GlStats.DataAccess.Repositories;

namespace GlStats.DataAccess;

public interface IUnitOfWork : IDisposable
{
    ITeamRepository TeamRepository { get; }
    ITeamMemberRepository TeamMemberRepository{ get; }
    void Commit(bool dryRun = false);
}