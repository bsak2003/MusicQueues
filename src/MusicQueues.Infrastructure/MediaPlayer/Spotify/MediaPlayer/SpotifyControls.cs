using System;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.MediaPlayer;

public class SpotifyControls : IPlayerControls
{
    public Task Pause(Guid queueId)
    {
        throw new NotImplementedException();
    }

    public Task Unpause(Guid queueId)
    {
        throw new NotImplementedException();
    }
}