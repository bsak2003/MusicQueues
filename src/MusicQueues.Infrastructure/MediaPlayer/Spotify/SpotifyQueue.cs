using System;
using System.Buffers.Text;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyQueue
{
    public SpotifyQueue(Guid id)
    {
        QueueId = id;
        NewState();
    }
    
    public Guid QueueId { get; }
    public string State { get; private set; }
    public string AccessToken { get; private set; }
    public DateTime Issued { get; private set; }
    public DateTime Expires { get; private set; }
    public string RefreshToken { get; private set; }

    public string OwnerId { get; private set; }
    public string OwnerName { get; private set; }
    public Uri OwnerProfilePicture { get; private set; }
    
    public string NewState()
    {
        State = Guid.NewGuid().ToString();
        return State;
    }

    public void SetTokens(string accessToken, string refreshToken, int expires)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Issued = DateTime.UtcNow;
        Expires = Issued.AddSeconds(expires);
    }

    public void RefreshTokens(string accessToken, int expires)
    {
        AccessToken = accessToken;
        Issued = DateTime.UtcNow;
        Expires = Issued.AddSeconds(expires);
    }

    public void SetUserMetrics(string userId, string username, string pictureUrl)
    {
        OwnerId = userId;
        OwnerName = username;
        OwnerProfilePicture = new Uri(pictureUrl);
    }
}