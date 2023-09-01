
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
    private readonly string serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");
    private readonly string frontPort = Environment.GetEnvironmentVariable("FRONTEND_PORT");

    private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/spotifyCallback";
    // private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("SERVER_PORT")}/Spotify/callback";

    [HttpPost("GetSpotifyData")]
    public async Task<ActionResult<StringReturn>> Get(
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository
    )
    {
        try
        {

            var userJwt = jwt.Validate<UserJwtData>(data.Value);
            var user = await userRepository.FirstOrDefaultAsync(user =>
                user.Name == userJwt.Name
            );
            var token = await tokenRepository.FirstOrDefaultAsync(token =>
                token.User == user.Name && token.Streamer == "Spotify"
            );
            if (token != null)
            {
                return Ok(new StringReturn
                {
                    Data = "https://www.spotify.com/br-pt/premium/?utm_source=br-pt_brand_contextual-desktop_text&utm_medium=paidsearch&utm_campaign=alwayson_latam_br_premiumbusiness_core_brand+contextual-desktop+text+exact+br-pt+google&gad=1&gclid=Cj0KCQjw9MCnBhCYARIsAB1WQVXjK5l9TqUfhCh5QIvAUqGxe7z3PFbgTkdhTGqxKpytS49Ph_NfW6gaApksEALw_wcB&gclsrc=aw.ds"
                });
            }

            string scope = "user-read-private user-read-email";
            string state = Rand.GetRandomString(16);

            string client_id = Environment.GetEnvironmentVariable("CLIENT_ID");

            var path = $"https://accounts.spotify.com/authorize?response_type=code&client_id={client_id}&scope={scope}&redirect_uri={this.redirectCallback}&state={state}";
            return Ok(new StringReturn
            {
                Data = path
            });
        }
        catch (Exception exp)
        {
            System.Console.WriteLine($"erooooooooooooooooooooooooooooooooooooooooooo\n {exp}");
            return BadRequest($"{exp}");
        }
    }

    [HttpPost("callback")]
    public async Task<ActionResult> Callback(
        [FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    )
    {
        string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
        string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

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

        var jwtUser = jwt.Validate<UserJwtData>(data.jwt);
        var user = await userRepository.FirstOrDefaultAsync(user =>
            user.Name == jwtUser.Name
        );

        System.Console.WriteLine("response: " + response);
        var token = await response.Content.ReadFromJsonAsync<SpotifyToken>();
        System.Console.WriteLine($"Token: {token.access_token}\n Refresh: {token.refresh_token}\n Scope: {token.scope}\n Type: {token.token_type}");
        try
        {
            Token _token = new()
            {
                User = user.Name,
                Streamer = "Spotify",
                StreamerToken = token.access_token,
                RefreshToken = token.refresh_token,
            };
            await tokenRepository.add(_token);

            return Ok();
        }
        catch (Exception exp)
        {
            return BadRequest($"{exp}");
        }
    }

    [HttpPost("RefreshToken")]
    public async Task<SpotifyToken> RefreshToken(
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        SpotifyToken token
    )
    {
        string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
        string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

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
    public async Task GetMusicData(
        [FromServices] HttpClient client,
        [FromBody] String accessToken
        )
    {
        string authorization = $"Bearer {accessToken}";
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        var music = await client.GetAsync("https://api.spotify.com/v1/tracks/2TpxZ7JUBn3uw46aR7qd6V");
        var content = await music.Content.ReadAsStringAsync();

        System.Console.WriteLine(content);
    }
}