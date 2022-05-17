using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Boundaries.UseCases.GetCurrentUser;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Core.UseCases;

public class GetCurrentUserUseCase : IGetCurrentUserUseCase
{
    private readonly IGetCurrentUserOutputPort _output;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetCurrentUserUseCase(IGetCurrentUserOutputPort output, ICurrentUserProvider currentUserProvider)
    {
        _output = output;
        _currentUserProvider = currentUserProvider;
    }

    public async Task Execute()
    {
        try
        {
            var currentUser = await _currentUserProvider.GetCurrentUserAsync();
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