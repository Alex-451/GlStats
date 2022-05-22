using GlStats.ApiWrapper.Entities.Api;

namespace GlStats.ApiWrapper.Entities.Responses.Users;

public class User
{
    public IEnumerable<UserResponse> Nodes { get; set; }
}