using System.Collections.Generic;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

public class RefreshTokens
{
    public string GrantType { get; set; }
    public string RefreshToken { get; set; }

    public RefreshTokens(string refreshToken)
    {
        GrantType = "refresh_token";
        RefreshToken = refreshToken;
    }
    
    public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
    {
        return new KeyValuePair<string, string>[]
        {
            new("grant_type", GrantType),
            new("refresh_token", RefreshToken)
        };
    }
}