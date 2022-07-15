using System.Collections.Generic;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;

public class RefreshTokensRequest
{
    public string GrantType { get; set; }
    public string RefreshToken { get; set; }

    public RefreshTokensRequest(string refreshToken)
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