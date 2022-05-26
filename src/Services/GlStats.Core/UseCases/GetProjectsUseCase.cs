using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.GetProjects;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class GetProjectsUseCase : IGetProjectsUseCase
{
    private readonly IGetProjectsOutputPort _output;
    private readonly IGitLabProvider _gitLabProvider;

    public GetProjectsUseCase(IGetProjectsOutputPort output, IGitLabProvider gitLabProvider)
    {
        _output = output;
        _gitLabProvider = gitLabProvider;
    }

    public async Task ExecuteAsync(GetProjectsInput input)
    {
        try
        {
            var projects = await _gitLabProvider.GetProjectsAsync(new ProjectSearchOptions
            {
                After = input.After,
                Before = input.Before,
                Search = input.Search,
            });

            _output.Default(projects);
        }
        catch (NoConnectionException)
        {
            _output.NoConnection();
        }
        catch (InvalidConfigException)
        {
            _output.InvalidConfig();
        }
        catch
        {
            _output.InvalidConfig();
        }
    }
}