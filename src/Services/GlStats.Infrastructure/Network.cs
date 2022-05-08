using System.Net.Http.Headers;
using System.Text;
using GlStats.Core.Boundaries.Infrastructure;

namespace GlStats.Infrastructure;

public class Network : INetwork
{
    private readonly HttpClient _client;

    public Network(HttpClient client)
    {
        _client = client;
    }


    public async Task<string> PostAsync(string url, string apiKey, string body)
    {
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (!_client.DefaultRequestHeaders.Contains("Authorization"))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        var response = await _client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

        return await response.Content.ReadAsStringAsync();
    }
}