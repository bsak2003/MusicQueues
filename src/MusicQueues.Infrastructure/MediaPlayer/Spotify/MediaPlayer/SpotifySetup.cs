using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.MediaPlayer;

public class SpotifySetup : IPlayerSetup
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly SpotifyConfig _config;
    private readonly ILogger<SpotifyMediaPlayer> _logger;

    public SpotifySetup(IRepository<SpotifyQueue> repository, SpotifyConfig config, ILogger<SpotifyMediaPlayer> logger)
    {
        _repository = repository;
        _config = config;
        _logger = logger;
    }

    public async Task<Uri> Setup(Guid queueId)
    {
        var sq = await _repository.ReadById(queueId);
        if (sq is null)
        {
            sq = new SpotifyQueue(queueId);
            await _repository.Create(sq);
        }
        else
        {
            sq.NewState();
        }

        var req = new AuthorizationRequest(_config.ClientId, _config.RedirectUri, sq.State);
        _logger.LogInformation($"setup requested with callback URI: {req.GetUri().AbsoluteUri}");
        return req.GetUri();
    }
}