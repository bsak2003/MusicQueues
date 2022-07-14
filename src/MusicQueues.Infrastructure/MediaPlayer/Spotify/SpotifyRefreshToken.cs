using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyRefreshToken : ICallbackHandler
{
    private readonly RefreshSpotifyTokens _refresh;
    private readonly IRepository<SpotifyQueue> _repository;

    public SpotifyRefreshToken(RefreshSpotifyTokens refresh, IRepository<SpotifyQueue> repository)
    {
        _refresh = refresh;
        _repository = repository;
    }

    public IEnumerable<string> Handles { get; } = new[] { "spotify-refresh" };
    public async Task<IActionResult> Handle(HttpContext context)
    {
        var qId = Guid.Parse(context.Request.Query["id"].ToString());
        await _refresh.Refresh(qId);

        return new OkObjectResult(await _repository.ReadById(qId));
    }
}