using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class RefreshSpotifyTokens
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly AuthApiClient _authApiClient;
    
    public RefreshSpotifyTokens(IRepository<SpotifyQueue> repository, AuthApiClient authApiClient)
    {
        _repository = repository;
        _authApiClient = authApiClient;
    }

    public async Task Refresh(Guid queueId)
    {
        var queue = await _repository.ReadById(queueId);

        var tokens = await _authApiClient.RefreshTokens(queue.RefreshToken);
        queue.RefreshTokens(tokens?.AccessToken, tokens.ExpiresIn);

        await _repository.Update(queue);
    }
}