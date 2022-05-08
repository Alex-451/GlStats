using System.Text.Json;
using GlStats.ApiWrapper.Entities.Api;
using GlStats.ApiWrapper.Entities.Responses;
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

    public bool IsAuthenticated() => !string.IsNullOrWhiteSpace(_auth.GetConfig().GitLabToken);
}