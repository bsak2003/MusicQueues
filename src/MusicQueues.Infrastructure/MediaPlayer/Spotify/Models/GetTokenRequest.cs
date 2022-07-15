using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Models;

public class GetTokenRequest
{
    public string Code { get; set; }
    public string RedirectUri { get; set; }
    public string GrantType { get; set; }

    public GetTokenRequest(string code, string redirectUri)
    {
        Code = code;
        RedirectUri = redirectUri;
        GrantType = "authorization_code";
    }
    
    public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
    {
        return new KeyValuePair<string, string>[]
        {
            new("code", Code),
            new("redirect_uri", RedirectUri),
            new("grant_type", GrantType)
        };
    }
}