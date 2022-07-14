using System;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.MediaPlayer;

public class SpotifyRefresh : IMediaRefresh
{
    public Task Hold(Guid queueId)
    {
        throw new NotImplementedException();
    }

    public Task Load(Guid queueId)
    {
        throw new NotImplementedException();
    }
}