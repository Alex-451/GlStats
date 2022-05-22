using System.Text.Json;
using GlStats.ApiWrapper.Entities.Api;
using GlStats.ApiWrapper.Entities.Responses;
using GlStats.ApiWrapper.Entities.Responses.Users;
using GlStats.Core.Boundaries.Infrastructure;

namespace GlStats.ApiWrapper;

public class GitLabClient : IGitLabClient
{
    private readonly INetwork _network;
    private readonly IAuthentication _auth;

    public GitLabClient(INetwork network, IAuthentication auth)
    {
        _network = network;
        _auth = auth;
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync()
    {
        var currentUser = await GetCurrentUserResponseAsync();

        return currentUser.Data.CurrentUser;
    }

    public async Task<IEnumerable<UserResponse>> SearchUsersAsync(string search)
    {
        var users = await SearchUsersResponseAsync(search);
        return users.Data.Users.Nodes;
    }

    private async Task<DataResponse<CurrentUserDataResponse>> GetCurrentUserResponseAsync()
    {
        var queryObject = new
        {
            query = @"{ 
                currentUser { 
                id,
                name
                }
            }",
            variables = new { }
        };

        var query = JsonSerializer.Serialize(queryObject);

        string json = await _network.PostAsync($@"{_auth.GetConfig().GitLabUrl}/api/graphql/", _auth.GetConfig().GitLabToken, query);
        return JsonSerializer.Deserialize<DataResponse<CurrentUserDataResponse>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    private async Task<DataResponse<UserDataResponse>> SearchUsersResponseAsync(string search)
    {
        var queryObject = new
        {
            query = @$"{{
                users(search: ""{search}"") {{
                    nodes {{
                        id
                        avatarUrl
                        username
                        name
                        publicEmail
                    }}
                }}
            }}"
        };

        var query = JsonSerializer.Serialize(queryObject);

        string json = await _network.PostAsync($@"{_auth.GetConfig().GitLabUrl}/api/graphql/", _auth.GetConfig().GitLabToken, query);
        return JsonSerializer.Deserialize<DataResponse<UserDataResponse>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    public bool IsAuthenticated() => !string.IsNullOrWhiteSpace(_auth.GetConfig().GitLabToken);


}