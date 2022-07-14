using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Web;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Requests;

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

    public StringContent GetStringContent()
    {
        
        return new StringContent(
            HttpUtility.UrlPathEncode($"code={Code}&redirect_uri={RedirectUri}&grant_type={GrantType}"),
            Encoding.UTF8,
            "application/x-www-form-urlencoded"
        );
    }
    
    public IEnumerable<KeyValuePair<string, string>> ToUrlEncoded()
    {
        return new KeyValuePair<string, string>[]
        {
            new("code", Code),
            new("redirect_uri", RedirectUri),
            new("grant_type", GrantType)
        };
    }
}