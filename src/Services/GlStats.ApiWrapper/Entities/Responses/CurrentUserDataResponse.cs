using System.Text.Json.Serialization;
using GlStats.ApiWrapper.Entities.Api;

namespace GlStats.ApiWrapper.Entities.Responses;

public class CurrentUserDataResponse
{
    public CurrentUserResponse CurrentUser { get; set; }
}