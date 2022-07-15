using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyCallbackHandler : ICallbackHandler
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly AuthApiClient _authApiClient;
    private readonly ApiClient _apiClient;
    private readonly SystemQueueStatus _systemQueueStatus;

    public SpotifyCallbackHandler(IRepository<SpotifyQueue> repository, AuthApiClient authApiClient, ApiClient apiClient, SystemQueueStatus systemQueueStatus)
    {
        _authApiClient = authApiClient;
        _apiClient = apiClient;
        _systemQueueStatus = systemQueueStatus;
        _repository = repository;
    }

    public IEnumerable<string> Handles { get; } = new string[] { "spotify-login", "spotify" };
    
    public async Task<IActionResult> Handle(HttpContext context)
    {
        var sessions = await _repository.ReadAll();
        
        var queue = sessions.First(x => x.State == context.Request.Query["state"]);
        
        var tokens = await _authApiClient.GetTokens(context.Request.Query["code"]);
        var userMetrics = await _apiClient.GetUserMetrics(tokens.AccessToken);
        
        queue.SetTokens(tokens?.AccessToken, tokens?.RefreshToken, tokens.ExpiresIn);
        queue.SetUserMetrics(userMetrics?.Id, userMetrics?.DisplayName, userMetrics?.Images.FirstOrDefault()?.Url);
        
        await _repository.Update(queue);

        await _systemQueueStatus.Update(queue.QueueId, Status.Authenticated);
        
        return new RedirectResult("https://localhost:5001/swagger");
    }
}