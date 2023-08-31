
namespace music_api.Controllers;

using Amazon.SecurityToken.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using music_api;
using music_api.Auxi;
using music_api.DTO;
using music_api.Model;

[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class SpotifyController : ControllerBase
{
    private readonly string serverPort  = Environment.GetEnvironmentVariable("SERVER_PORT");
    private readonly string frontPort   = Environment.GetEnvironmentVariable("FRONTEND_PORT");
    
    private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/spotifyCallback";
    // private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("SERVER_PORT")}/Spotify/callback";

    [HttpGet("GetSpotifyData")]
    public StringReturn Get()
    {
        Random rand = new Random();
        var spotify = new MySpotify();
        spotify.SetAccessToken(Environment.GetEnvironmentVariable("CLIENT_ID"));
        var newSpotify = spotify.GetClient();

        var track = newSpotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH");
        System.Console.WriteLine(track);

        string scope = "user-read-private user-read-email";
        string state = Rand.GetRandomString(16);

        string client_id = Environment.GetEnvironmentVariable("CLIENT_ID");

        var path = $"https://accounts.spotify.com/authorize?response_type=code&client_id={client_id}&scope={scope}&redirect_uri={this.redirectCallback}&state={state}";
        Console.WriteLine($"\n\n{path}");
        // Response.Redirect(path);
        return new StringReturn{
            Data = path
        };
    }

    [HttpPost("callback")]
    public async Task<ActionResult> Callback(
        [FromServices]HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    )
    {
        string clientId      = Environment.GetEnvironmentVariable("CLIENT_ID");
        string clientSecret  = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        string dataClient = $"{clientId}:{clientSecret}";
        dataClient = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataClient));
        string authorization = $"Basic {dataClient}";
        string contentType = "application/x-www-form-urlencoded";
               
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        client.DefaultRequestHeaders.Add("ContentType", contentType);

        var formData = new List<KeyValuePair<string, string>>();
        formData.Add(new KeyValuePair<string, string>("code", $"{data.code}"));
        formData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
        formData.Add(new KeyValuePair<string, string>("redirect_uri", this.redirectCallback));

        var body = new FormUrlEncodedContent(formData);
        var response = await client.PostAsync("https://accounts.spotify.com/api/token", body);

        var jwtUser = jwt.Validate<ReturnLoginData>(data.jwt);
        var user = await userRepository.FirstOrDefaultAsync( user => 
            user.Name == jwtUser.Name
        );


        var token = await response.Content.ReadFromJsonAsync<SpotifyToken>();
        try{
            //TODO: terminar com tokens
            await tokenRepository.add(new Token{
                User = user.Name,
                Streamer = "Spotify",
                StreamerToken = "",
                RefreshToken = ""
            });
            return Ok();
        }
        catch (Exception exp) {
            return BadRequest("{exp}");
        }
    }

    [HttpPost("RefreshToken")]
    public async Task<SpotifyToken> RefreshToken (
        [FromServices]HttpClient client,
        [FromServices]IRepository<Token> tokenRepository,
        [FromServices]IRepository<User> userRepository,
        SpotifyToken token
    ) 
    {
        string clientId      = Environment.GetEnvironmentVariable("CLIENT_ID");
        string clientSecret  = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        string dataClient = $"{clientId}:{clientSecret}";
        dataClient = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataClient));
        string authorization = $"Basic {dataClient}";
        client.DefaultRequestHeaders.Add("Authorization", authorization);

        var newForm = new List<KeyValuePair<string, string>>();
        newForm.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
        newForm.Add(new KeyValuePair<string, string>("refresh_token", $"{token.refresh_token}"));
        
        var newBody = new FormUrlEncodedContent(newForm);
        var refreshToken = await client.PostAsync("https://accounts.spotify.com/api/token", newBody);

        var result = await refreshToken.Content.ReadFromJsonAsync<SpotifyToken>();



        return result;
    }

    [HttpPost("GetMusicData")]
    public async Task GetMusicData ( 
        [FromServices]HttpClient client, 
        [FromBody]String accessToken 
        ) 
    {
        string authorization = $"Bearer {accessToken}";
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        var music = await client.GetAsync("https://api.spotify.com/v1/tracks/2TpxZ7JUBn3uw46aR7qd6V");
        var content = await music.Content.ReadAsStringAsync();

        System.Console.WriteLine(content);
    }
}