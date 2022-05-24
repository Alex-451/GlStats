namespace GlStats.Core.Boundaries.UseCases.GetUsersById;

public interface IGetUsersByIdsUseCase
{
    Task ExecuteAsync(string[] ids);
}