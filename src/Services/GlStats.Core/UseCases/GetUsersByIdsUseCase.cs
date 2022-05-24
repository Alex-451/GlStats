using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.GetUsersById;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class GetUsersByIdsUseCase : IGetUsersByIdsUseCase
{
    private readonly IGetUsersByIdsOutputPort _output;
    private readonly IGitLabProvider _gitLabProvider;

    public GetUsersByIdsUseCase(IGetUsersByIdsOutputPort output, IGitLabProvider gitLabProvider)
    {
        _output = output;
        _gitLabProvider = gitLabProvider;
    }

    public async Task ExecuteAsync(string[] ids)
    {
        try
        {
            var users = await _gitLabProvider.GetUsersByIdsAsync(ids);
            _output.Default(users);
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