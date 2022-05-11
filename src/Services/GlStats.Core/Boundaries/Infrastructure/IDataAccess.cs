using System.Data;

namespace GlStats.Core.Boundaries.Infrastructure;

public interface IDataAccess
{
    Task<List<T>> LoadDataAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null);
    Task SaveDataAsync(string sql, object? parameters = null, IDbTransaction? transaction = null);
}