using System;
using System.Threading.Tasks;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

public class AuthorizationRequest
{
    // todo other requests, HttpClient, constructors
    public string ResponseType { get; set; }
    public string ClientId { get; set; }
    public string Scope { get; set; }
    public string RedirectUri { get; set; }
    public string State { get; set; }

    public AuthorizationRequest(string clientId, string redirectUri, string state)
    {
        ResponseType = "code";
        Scope = "user-read-private user-read-email";

        ClientId = clientId;
        RedirectUri = redirectUri;
        State = state;
    }

    public Uri GetUri()
    {
        const string spotify = "https://accounts.spotify.com/authorize?";
        var query = $"response_type={ResponseType}&client_id={ClientId}&scope={Scope}&redirect_uri={RedirectUri}&state={State}";
        return new Uri(spotify + query);
    }
}