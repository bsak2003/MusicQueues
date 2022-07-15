using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;

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

    public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
    {
        return new KeyValuePair<string, string>[]
        {
            new("response_type", ResponseType),
            new("client_id", ClientId),
            new("scope", Scope),
            new("redirect_uri", RedirectUri),
            new("state", State)
        };
    }
}