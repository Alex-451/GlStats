namespace GlStats.Core.Boundaries.UseCases.SearchUsers;

public interface ISearchUsersUseCase
{
    Task ExecuteAsync(string search);
}