using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class RefreshSpotifyTokens
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly SpotifyConfig _config;
    
    public RefreshSpotifyTokens(IRepository<SpotifyQueue> repository, SpotifyConfig config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task Refresh(Guid queueId)
    {
        var queue = await _repository.ReadById(queueId);
        
        var client = new HttpClient();
        
        var auth = $"{_config.ClientId}:{_config.ClientSecret}";
        var basicAuthentication = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(auth))}";
        client.DefaultRequestHeaders.Add("Authorization", basicAuthentication);

        var request = new RefreshTokens(queue.RefreshToken);
        var post = await client.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(request.ToKeyValuePairs()));

        var tokens = JsonSerializer.Deserialize<Tokens>(await post.Content.ReadAsStringAsync());
        
        queue.RefreshTokens(tokens?.AccessToken, tokens.ExpiresIn);

        await _repository.Update(queue);
    }
}