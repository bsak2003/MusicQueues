using System;
using System.Buffers.Text;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyQueue
{
    public SpotifyQueue(Guid id)
    {
        QueueId = id;
        State = Guid.NewGuid().ToString();
    }
    
    public Guid QueueId { get; }
    public string State { get; private set; }
    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }

    public string NewState()
    {
        State = Guid.NewGuid().ToString();
        return State;
    }

    public void SetAccessToken(string token)
    {
        AccessToken = token;
    }

    public void SetRefreshToken(string token)
    {
        RefreshToken = token;
    }
}