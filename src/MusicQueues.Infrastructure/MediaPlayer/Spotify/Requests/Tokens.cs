using System.Text.Json.Serialization;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

public class Tokens
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; }
    [JsonPropertyName("scope")]
    public string Scope { get; init; }
}