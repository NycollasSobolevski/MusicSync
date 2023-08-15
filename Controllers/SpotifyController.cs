
namespace music_api.Controllers;

using Microsoft.AspNetCore.Mvc;
using music_api;
using music_api.Auxi;

[ApiController]
[Route("[controller]")]
public class SpotifyController : ControllerBase
{
    private readonly string serverPort  = Environment.GetEnvironmentVariable("SERVER_PORT");

    [HttpGet( Name = "GetSpotifyData" )]
    public void Get()
    {
        Random rand = new Random();
        var spotify = new MySpotify();
        spotify.SetAccessToken("token");
        var newSpotify = spotify.GetClient();

        var track = newSpotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH");
        System.Console.WriteLine(track);

        string scope = "user-read-private user-read-email";
        string state = Rand.GetRandomString(16);
 
        string redirect = "";
        string client_id = "";
 
        Response.Redirect($"https://accounts.spotify.com/authorize?response_type=code&{client_id}&scope={scope}&redirect_uri={redirect}&state={state}");
    }

    [HttpGet(Name = "callback")]
    public void Callback(string code, string state)
    {
        
    }
}