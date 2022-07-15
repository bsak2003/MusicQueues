using System.Text.Json.Serialization;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;

public class UserMetrics
{
    [JsonPropertyName("country")]
    public string Country { get; init; }
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; }
    [JsonPropertyName("email")]
    public string Email { get; init; }
    [JsonPropertyName("explicit_content")]
    public ExplicitContent ExplicitContent { get; init; }
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; init; }
    [JsonPropertyName("followers")]
    public Followers Followers { get; init; }
    [JsonPropertyName("href")]
    public string Href { get; init; }
    [JsonPropertyName("id")]
    public string Id { get; init; }
    [JsonPropertyName("images")]
    public Image[] Images { get; init; }
    [JsonPropertyName("product")]
    public string Product { get; init; }
    [JsonPropertyName("type")]
    public string Type { get; init; }
    [JsonPropertyName("uri")]
    public string Uri { get; init; }
}

public class ExplicitContent
{
    [JsonPropertyName("filter_enabled")]
    public bool FilterEnabled { get; init; }
    [JsonPropertyName("filter_locked")]
    public bool FilterLocked { get; init; }
}

public class ExternalUrls
{
    [JsonPropertyName("spotify")]
    public string Spotify { get; init; }
}

public class Followers
{
    [JsonPropertyName("href")]
    public string Href { get; init; }
    [JsonPropertyName("total")]
    public int Total { get; init; }
}

public class Image
{
    [JsonPropertyName("url")]
    public string Url { get; init; }
    [JsonPropertyName("height")]
    public int? Height { get; init; }
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}