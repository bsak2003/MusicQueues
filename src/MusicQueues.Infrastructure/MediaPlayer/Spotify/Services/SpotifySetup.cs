using System;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class SpotifySetup : IPlayerSetup
{
    private readonly IRepository<SpotifyQueue> _repository;
    private readonly SpotifyConfig _config;

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
        else
        {
            sq.NewState();
        }
        
        // TODO finish

        throw new NotImplementedException();
    }
}