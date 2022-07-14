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
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyCallbackHandler : ICallbackHandler
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly IRepository<Queue> _queueRepository;
    private readonly SpotifyConfig _config;
    private readonly ILogger<SpotifyMediaPlayer> _logger;

    public SpotifyCallbackHandler(IRepository<SpotifyQueue> repository, SpotifyConfig config, ILogger<SpotifyMediaPlayer> logger, IRepository<Queue> queueRepository)
    {
        _config = config;
        _logger = logger;
        _queueRepository = queueRepository;
        _repository = repository;
    }

    public IEnumerable<string> Handles { get; } = new string[] { "spotify-login", "spotify" };
    
    public async Task<IActionResult> Handle(HttpContext context)
    {
        var code = context.Request.Query["code"].ToString();
        var state = context.Request.Query["state"].ToString();

        var sessions = await _repository.ReadAll();
        var qId = sessions.First(x => x.State == state).QueueId;

        var req = new GetTokenRequest(code, _config.RedirectUri);
        var client = new HttpClient();

        var auth = $"{_config.ClientId}:{_config.ClientSecret}";
        var basicAuthentication = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(auth))}";
        client.DefaultRequestHeaders.Add("Authorization", basicAuthentication);

        var content = new FormUrlEncodedContent(req.ToUrlEncoded());
        var post = await client.PostAsync("https://accounts.spotify.com/api/token", content);
        var tokens = JsonSerializer.Deserialize<Tokens>(await post.Content.ReadAsStringAsync());

        var queue = await _repository.ReadById(qId);
        queue.SetTokens(tokens?.AccessToken, tokens?.RefreshToken, tokens.ExpiresIn);

        var client2 = new HttpClient();
        client2.DefaultRequestHeaders.Add("Authorization", $"Bearer {queue.AccessToken}");
        var get = await client2.GetAsync("https://api.spotify.com/v1/me");
        var obj = JsonSerializer.Deserialize<UserMetrics>(await get.Content.ReadAsStringAsync());
        queue.SetUserMetrics(obj?.Id, obj?.DisplayName, obj?.Images.FirstOrDefault()?.Url);

        await _repository.Update(queue);

        var realQueue = await _queueRepository.ReadById(qId);
        realQueue.UpdateStatus(Status.Authenticated);
        await _queueRepository.Update(realQueue);

        return new OkObjectResult(queue);
        // return new RedirectResult("https://localhost:5001/swagger");
    }
}