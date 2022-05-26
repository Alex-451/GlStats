namespace GlStats.Core.Boundaries.UseCases.GetProjects;

public interface IGetProjectsUseCase
{
    Task ExecuteAsync(GetProjectsInput input);
}