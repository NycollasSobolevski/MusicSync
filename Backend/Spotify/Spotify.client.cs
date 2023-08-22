
using SpotifyAPI.Web;

namespace music_api;

public class MySpotify
{
    private string accesToken { get; set; } = "";

    public MySpotify() {}

    public string GetAccessToken()
        => accesToken;

    public void SetAccessToken(string token)
        => this.accesToken = token;

    public SpotifyClient GetClient()
    {
        var spotify = new SpotifyClient(accesToken);
        return spotify;
    }
}