using System;
using System.Net.Http;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.MediaPlayer;

public class SpotifySetup : IPlayerSetup
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly SpotifyConfig _config;

    private const string AuthEndpoint = "https://accounts.spotify.com/authorize";

    public SpotifySetup(IRepository<SpotifyQueue> repository, SpotifyConfig config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task<Uri> Setup(Guid queueId)
    {
        var sq = await _repository.ReadById(queueId);
        if (sq is null)
        {
            sq = new SpotifyQueue(queueId);
            await _repository.Create(sq);
        }
        
        var req = new AuthorizationRequest(_config.ClientId, _config.RedirectUri, sq.NewState());
        
        var query = new FormUrlEncodedContent(req.ToKeyValuePairs());
        var uri = new UriBuilder(AuthEndpoint)
        {
            Query = await query.ReadAsStringAsync()
        };
        
        return uri.Uri; 
    }
}