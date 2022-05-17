using GlStats.ApiWrapper;
using GlStats.ApiWrapper.Entities.Api;
using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Infrastructure.Providers;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IGitLabClient _client;

    public CurrentUserProvider(IGitLabClient client)
    {
        _client = client;
    }

    public async Task<CurrentUser> GetCurrentUserAsync()
    {
        if (!_client.IsAuthenticated())
            throw new InvalidConfigException();

        try
        {
            var currentUser = await _client.GetCurrentUserAsync();

            if (string.IsNullOrWhiteSpace(currentUser.Id))
                throw new InvalidConfigException();

            return ToCurrentUser(currentUser);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidConfigException();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw new NoConnectionException();
        }
    }

    private CurrentUser ToCurrentUser(CurrentUserResponse response)
    {
        return new CurrentUser
        {
            Id = response.Id,
            Name = response.Name
        };
    }
}