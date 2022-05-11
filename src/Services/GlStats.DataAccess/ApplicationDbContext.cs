using GlStats.DataAccess.Entities;
using GlStats.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GlStats.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var test = new JsonConfiguration();
        var connection = new SqliteConnectionStringBuilder { DataSource = test.GetConfig().ConnectionString };
        optionsBuilder.UseSqlite(connection.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

}