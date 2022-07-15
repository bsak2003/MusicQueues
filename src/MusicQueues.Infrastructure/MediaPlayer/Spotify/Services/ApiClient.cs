using System;
using System.Net.Http;
using System.Threading.Tasks;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;
using System.Text.Json;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class ApiClient
{
    private const string Endpoint = "https://api.spotify.com";
    private const string UserDataPath = "/v1/me";

    private readonly IHttpClientFactory _factory;
    
    public ApiClient(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    private HttpClient GetClient(string accessToken)
    {
        var client = _factory.CreateClient("SpotifyApiClient");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        client.BaseAddress = new Uri(Endpoint);

        return client;
    }

    public async Task<UserMetrics> GetUserMetrics(string accessToken)
    {
        var client = GetClient(accessToken);
        var get = await client.GetAsync(UserDataPath);
        return JsonSerializer.Deserialize<UserMetrics>(await get.Content.ReadAsStringAsync());
    }
}