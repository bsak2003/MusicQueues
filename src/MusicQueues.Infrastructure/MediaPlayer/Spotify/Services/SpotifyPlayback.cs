using System;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class SpotifyPlayback : IMediaPlayback
{
    public Task Start(Guid queueId)
    {
        throw new NotImplementedException();
    }

    public Task Stop(Guid queueId)
    {
        throw new NotImplementedException();
    }
}