namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

public class AuthorizationRequest
{
    // todo other requests, HttpClient, constructors
    public string ResponseType { get; set; }
    public string ClientId { get; set; }
    public string Scope { get; set; }
    public string RedirectUri { get; set; }
    public string State { get; set; }
}