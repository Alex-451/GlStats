using GlStats.Core.Boundaries.Infrastructure;
using GlStats.DataAccess.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GlStats.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Team>? Teams { get; set; }

    //todo dependency injection causes issues when trying to create migrations (should be removed I guess)
    private readonly IAuthentication _auth;

    public ApplicationDbContext(IAuthentication auth)
    {
        _auth = auth;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = new SqliteConnectionStringBuilder { DataSource = _auth.GetConfig().ConnectionString };
        optionsBuilder.UseSqlite(connection.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}