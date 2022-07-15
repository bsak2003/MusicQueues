using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class AuthApiClient
{
    private const string Endpoint = "https://accounts.spotify.com/api/token";

    private readonly SpotifyConfig _config;
    private readonly HttpClient _client;
    private readonly Uri _uri;

    public AuthApiClient(IHttpClientFactory factory, SpotifyConfig config)
    {
        _config = config;
        
        _client = factory.CreateClient("SpotifyAuthApiClient");
        
        var auth = $"{_config.ClientId}:{_config.ClientSecret}";
        var basicAuthentication = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(auth))}";
        _client.DefaultRequestHeaders.Add("Authorization", basicAuthentication);

        _uri = new Uri(Endpoint);
    }

    public async Task<Tokens> GetTokens(string code)
    {
        var request = new GetTokenRequest(code, _config.RedirectUri);
        
        var post = await _client.PostAsync(_uri, new FormUrlEncodedContent(request.ToKeyValuePairs()));
        
        return JsonSerializer.Deserialize<Tokens>(await post.Content.ReadAsStringAsync());
    }

    public async Task<Tokens> RefreshTokens(string refreshToken)
    {
        var request = new RefreshTokensRequest(refreshToken);
        
        var post = await _client.PostAsync(_uri, new FormUrlEncodedContent(request.ToKeyValuePairs()));
        
        return JsonSerializer.Deserialize<Tokens>(await post.Content.ReadAsStringAsync());
    }
}