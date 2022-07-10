using Microsoft.Extensions.Configuration;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyConfig
{
    public SpotifyConfig(IConfiguration configuration)
    {
        ClientId = configuration["spotify:client_id"];
        ClientSecret = configuration["spotify:client_secret"];
        RedirectUri = configuration["spotify:redirect_uri"];
    }
    
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUri { get; }
}