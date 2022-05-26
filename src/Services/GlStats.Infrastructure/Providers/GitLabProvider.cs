using GlStats.ApiWrapper;
using GlStats.ApiWrapper.Entities.Api;
using GlStats.ApiWrapper.Entities.Requests;
using GlStats.Core.Boundaries.Providers;
using GlStats.Core.Entities;
using GlStats.Core.Entities.Exceptions;

namespace GlStats.Infrastructure.Providers;

public class GitLabProvider : IGitLabProvider
{
    private readonly IGitLabClient _client;

    public GitLabProvider(IGitLabClient client)
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

    public async Task<IEnumerable<User>> SearchUsersAsync(string search)
    {
        if (!_client.IsAuthenticated())
            throw new InvalidConfigException();

        try
        {
            var currentUser = await _client.SearchUsersAsync(search);
            return currentUser.Select(ToUser);
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

    public async Task<IEnumerable<User>> GetUsersByIdsAsync(string[] ids)
    {
        if (!_client.IsAuthenticated())
            throw new InvalidConfigException();

        try
        {
            var users = await _client.GetUsersByIdAsync(ids);
            return users.Select(ToUser);
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

    public async Task<IEnumerable<Project>> GetProjectsAsync(ProjectSearchOptions options)
    {
        if (!_client.IsAuthenticated())
            throw new InvalidConfigException();

        try
        {
            var projects = await _client.GetProjectsAsync(new ProjectQueryOptions(options));
            return projects.Select(ToProject);
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
        return new()
        {
            Id = response.Id,
            Name = response.Name
        };
    }

    private User ToUser(UserResponse response)
    {
        return new()
        {
            Id = response.Id,
            AvatarUrl = response.AvatarUrl,
            Username = response.Username,
            Name = response.Name,
            PublicEmail = response.PublicEmail,
        };
    }

    private Project ToProject(ProjectResponse response)
    {
        return new()
        {
            Id = response.Id,
            Name = response.Name,
            AvatarUrl = response.AvatarUrl,
            Description = response.Description,
            WebUrl = response.WebUrl,
        };
    }
}