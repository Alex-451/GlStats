using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.SearchUsers;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class SearchUsersUseCase : ISearchUsersUseCase
{
    private readonly ISearchUsersOutputPort _output;
    private readonly IGitLabProvider _gitLabProvider;

    public SearchUsersUseCase(ISearchUsersOutputPort output, IGitLabProvider gitLabProvider)
    {
        _output = output;
        _gitLabProvider = gitLabProvider;
    }

    public async Task ExecuteAsync(string search)
    {
        try
        {
            var currentUser = await _gitLabProvider.SearchUsersAsync(search);
            _output.Default(currentUser);
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