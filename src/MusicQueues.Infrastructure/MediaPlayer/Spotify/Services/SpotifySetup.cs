using System;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class SpotifySetup : IPlayerSetup
{
    public Task<Uri> Setup(Guid queueId)
    {
        throw new NotImplementedException();
    }
}