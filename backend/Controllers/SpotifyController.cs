
namespace music_api.Controllers;

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson.IO;
using music_api;
using music_api.Auxi;
using music_api.DTO;
using music_api.Model;
using Swan.Formatters;

[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class SpotifyController : ControllerBase
{
    private readonly string clientUrl = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_URL");
    private readonly string serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");
    private readonly string frontPort = Environment.GetEnvironmentVariable("FRONTEND_PORT");

    private readonly string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
    private readonly string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

    private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/Callback?streamer=spotify";
    // private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("SERVER_PORT")}/Spotify/callback";

    [HttpPost("GetData")]
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
                token.User == user.Name && token.Service == "Spotify"
            );
            if (token != null)
            {
                return Ok(new StringReturn
                {
                    Data = "http://localhost:4200/?tab=Spotify"
                });
            }

            string scope = """
                user-read-private 
                user-read-email 
                playlist-read-private 
                playlist-read-collaborative 
                playlist-read-private 
                user-read-private 
                user-read-email 
                playlist-modify-public 
                playlist-modify-private
            """;
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
            return BadRequest($"{exp}");
        }
    }
    [HttpPost("LogOff")]
    public async Task<ActionResult> LogOff(
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository
    )
    {
        var userJwt = jwt.Validate<UserJwtData>(data.Value);
        try
        {
            var user = await userRepository.FirstOrDefaultAsync(user =>
                user.Name == userJwt.Name
            );
            var token = await tokenRepository.FirstOrDefaultAsync(token =>
                token.User == user.Name && token.Service == "Spotify"
            );
            System.Console.WriteLine(token + " " + user.Name);
            if (token != null)
            {
                await tokenRepository.Delete(token);
                return Ok();
            }
            return BadRequest();
        }
        catch (Exception exp)
        {
            return BadRequest($"{exp}");
        }
    }

    [HttpPost("callback")]
    public async Task<ActionResult> Callback(
        [FromBody] CallbackData data,
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    )
    {
        string dataClient = $"{this.clientId}:{this.clientSecret}";
        dataClient = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataClient));

        client.DefaultRequestHeaders.Add("Authorization", $"Basic {dataClient}");
        client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");

        var formData = new List<KeyValuePair<string, string>>();
        formData.Add(new KeyValuePair<string, string>("code", $"{data.code}"));
        formData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
        formData.Add(new KeyValuePair<string, string>("redirect_uri", this.redirectCallback));
        try
        {
            var body = new FormUrlEncodedContent(formData);
            var response = await client.PostAsync("https://accounts.spotify.com/api/token", body);

            var jwtUser = jwt.Validate<UserJwtData>(data.jwt);
            var user = await userRepository.FirstOrDefaultAsync(user =>
                user.Name == jwtUser.Name
            );

            // System.Console.WriteLine("response: " + response);
            var token = await response.Content.ReadFromJsonAsync<SpotifyToken>();
            // System.Console.WriteLine($"Token: {token.access_token}\n Refresh: {token.refresh_token}\n Scope: {token.scope}\n Type: {token.token_type}");
        
            Token _token = new()
            {
                User = user.Name,
                Service = "Spotify",
                ServiceToken = token.access_token,
                RefreshToken = token.refresh_token,
                ExpiresIn = token.expires_in,
                LastUpdate = DateTime.Now
            };
            await tokenRepository.add(_token);

            return Ok();
        }
        catch (Exception exp)
        {  
            System.Console.WriteLine(exp);
            return BadRequest($"internal server error");
        }
    }

    [HttpPost("RefreshToken")]
    public async Task<ActionResult> RefreshToken(
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwtService,
        [FromBody] JWT jwt
    )
    {
        var jwtUser = jwtService.Validate<UserJwtData>(jwt.Value);
        var user  = await userRepository.FirstOrDefaultAsync(user => user.Email == jwtUser.Email);
        var token = await tokenRepository.FirstOrDefaultAsync(token => token.User == user.Name && token.Service == "Spotify");
        string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
        string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        string dataClient = $"{clientId}:{clientSecret}";
        dataClient = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataClient));
        string authorization = $"Basic {dataClient}";
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        

        var newForm = new List<KeyValuePair<string, string>>();
        newForm.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
        newForm.Add(new KeyValuePair<string, string>("refresh_token", $"{token.RefreshToken}"));

        try{

            var newBody = new FormUrlEncodedContent(newForm);
            var refreshToken = await client.PostAsync("https://accounts.spotify.com/api/token", newBody);
            
            if(refreshToken.StatusCode != HttpStatusCode.OK)
                return BadRequest("internal server error");

            var result = await refreshToken.Content.ReadFromJsonAsync<SpotifyToken>();
            token.ServiceToken = result.access_token;
            token.LastUpdate = DateTime.Now;
            await tokenRepository.Update( token );

            var returnBody = Json.Serialize("Token Updated");

            return Ok(returnBody);
        }
        catch(Exception exp){
            return BadRequest($"{exp}");
        }
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

    [HttpPost("GetUserPlaylists")]
    public async Task<ActionResult> GetUserPlaylists (
        [FromBody] JWTWithGetPlaylistData data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    )
    {
        var userJwt = jwt.Validate<UserJwtData>(data.Jwt.Value);
        if (data.Limit == 0)
            data.Limit = 20;
        try{
            var user = await userRepository.FirstOrDefaultAsync(
                u => u.Email == userJwt.Email
            );

            var spotifyToken = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == user.Name &&
                token.Service == "Spotify"
            );
            var userSpotifyReturn = await this.GetUserSpotify( client, spotifyToken.ServiceToken );
            // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {spotifyToken.ServiceToken}");
            var response = await client.GetAsync($"https://api.spotify.com/v1/me/playlists?offset={data.Offset}&limit={data.Limit}");
            var result = await response.Content.ReadAsStringAsync();
            
            if(response.StatusCode == HttpStatusCode.Unauthorized){
                return Unauthorized(await response.Content.ReadAsStringAsync());
            }
            if(response.StatusCode != HttpStatusCode.OK){
                return BadRequest(await response.Content.ReadAsStringAsync());
            }

            return Ok(result);
        }
        catch (Exception exp){
            return BadRequest(exp);
        }
    }

    [HttpPost("GetPlaylist/")]
    public async Task<ActionResult> GetPlaylist(
        [FromQuery(Name = "id")] string id,
        [FromBody] JWT body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    )
    {
        var userJwt = jwt.Validate<UserJwtData>(body.Value);
        try{
            var token = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == userJwt.Name &&
                token.Service == "Spotify"
            );
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.ServiceToken}");
            var response = await client.GetAsync($"https://api.spotify.com/v1/playlists/{id}");
            
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);
            if(response.StatusCode != HttpStatusCode.OK)
                return BadRequest(await response.Content.ReadAsStringAsync());
            return Ok(await response.Content.ReadAsStringAsync());
        }
        catch(Exception exp){
            return BadRequest(exp);
        }
    }

    [HttpPost("GetPlaylistTracks/")]
    public async Task<ActionResult> GetPlaylistTracks (
        [FromQuery(Name = "id")] string id,
        [FromQuery(Name = "streamer")] string streamer,
        [FromBody] JWT body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    )
    {

        var userJwt = jwt.Validate<UserJwtData>(body.Value);
        try{
            var token = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == userJwt.Name &&
                token.Service == "Spotify"
            );
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.ServiceToken}");
            var response = await client.GetAsync($"https://api.spotify.com/v1/playlists/{id}/tracks");
            
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);
            if(response.StatusCode != HttpStatusCode.OK)
                return BadRequest(await response.Content.ReadAsStringAsync());
            return Ok(await response.Content.ReadAsStringAsync());
        }
        catch(Exception exp){
            return BadRequest(exp);
        }
    }
    [HttpPost("GetMoreTracks")]
    public async Task<ActionResult> GetMoreTracks (
        [FromBody] JWTWithData<string> body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    )
    {
        var userJwt = jwt.Validate<UserJwtData>(body.Jwt.Value);
        try{

            var token = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == userJwt.Name &&
                token.Service == "Spotify"
            );
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.ServiceToken}");
            var response = await client.GetAsync($"{body.Data}");
            
            if (response.StatusCode == HttpStatusCode.Unauthorized){
                await this.refreshToken("irineu", client, tokenRepository);
                return Unauthorized(response);
            }
            if(response.StatusCode != HttpStatusCode.OK)
                return BadRequest(await response.Content.ReadAsStringAsync());
            return Ok(await response.Content.ReadAsStringAsync());

        } catch(Exception exp){
            return BadRequest(exp);
        }
    }

    [HttpPost("CreatePlaylist")]
    public async Task<ActionResult> CreatePlaylist (
        [FromBody] UserCreatePlaylistWithJwt data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] HttpClient client
    ){
        var userJwt = jwt.Validate<UserJwtData>(data.Jwt.Value);
        try{
            var user = await userRepository.FirstOrDefaultAsync(user => 
                user.Email == userJwt.Email ||
                user.Name == userJwt.Name
            );
            var token = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == user.Name &&
                token.Service == "Spotify"
            );

            var userSpotify = await this.GetUserSpotify(client, token.ServiceToken);

            System.Console.WriteLine(data.Data.name + data.Data.description + data.Data.@public);
            
            CreatePlaylistData bodyData = new () {
                name = data.Data.name,
                description = data.Data.description,
                @public = data.Data.@public
            };

            string json = JsonSerializer.Serialize(bodyData);
            StringContent httpContent = new (json, System.Text.Encoding.UTF8, "application/json");
            System.Console.WriteLine(json);
            var createResponse = await client.PostAsync($"{this.clientUrl}/users/{userSpotify.id}/playlists", httpContent);
             
            var obj = await createResponse.Content.ReadFromJsonAsync<SpotifyUserPlaylistItems>();
            var strin = await createResponse.Content.ReadAsStringAsync();

            System.Console.WriteLine(createResponse.StatusCode);
            System.Console.WriteLine(strin);

            return Ok(obj);
                        
        } catch (Exception exp){
            return BadRequest(exp);
        }
    }

    [HttpPost("AddTrackToPlaylist")]
    public async Task<ActionResult> AddTrackToPlaylist(
        [FromBody] JWTWithData<TrackAndPlaylist> data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] HttpClient client
    ){
        System.Console.WriteLine(data);
        try{
            var userJwt = jwt.Validate<UserJwtData>(data.Jwt.Value);
            var user = await userRepository.FirstOrDefaultAsync(user => 
                user.Email == userJwt.Email ||
                user.Name == userJwt.Name
            );
            var token = await tokenRepository.FirstOrDefaultAsync(token => 
                token.User == user.Name &&
                token.Service == "Spotify"
            );
            data.Data.Track.name = data.Data.Track.name.Replace(" ", "%2520");
            data.Data.Track.author = data.Data.Track.author.Replace(" ", "%2520");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.ServiceToken}");
            var responseSearch = await client.GetAsync($"{this.clientUrl}/search?q=remaster%2520track%3A{data.Data.Track.name}%2520artist%3A{data.Data.Track.author}&type=track&limit=1&offset=0");
            
            if(responseSearch.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized("Unauthorized");

            if(responseSearch.StatusCode != HttpStatusCode.OK)
                return BadRequest("internal server error");

            var obj = await responseSearch.Content.ReadFromJsonAsync<TrackSearchData>();
            var strin = await responseSearch.Content.ReadAsStringAsync();

            if(obj.tracks.items.Length == 0)
                return BadRequest("Track not found");

            var bodyData = new {
                uris = new string[] { obj.tracks.items[0].uri }
            };


            string json = JsonSerializer.Serialize(bodyData);
            StringContent httpContent = new (json, System.Text.Encoding.UTF8, "application/json");
            responseSearch = await client.PostAsync($"https://api.spotify.com/v1/playlists/{data.Data.PlaylistId}/tracks", httpContent);

            if(responseSearch.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized("Unauthorized");
            var res = Json.Serialize("Music Inserted");
            return Ok(res);
        } catch (Exception exp) {
            return BadRequest(exp);
        }
    }

    //* Private Methods
    private async Task<SpotifyUserData> GetUserSpotify (
        [FromServices] HttpClient client,
        string token
    )
    {
        string authorization = $"Bearer {token}";
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        try{
            var response = await client.GetAsync("https://api.spotify.com/v1/me");
            var content = await response.Content.ReadFromJsonAsync<SpotifyUserData>();
            // System.Console.WriteLine(content);
            return content;
        }
        catch(Exception exp){
            System.Console.WriteLine(exp);
            return null;
        }
    }

    private async Task refreshToken (
        string username,
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository
    )
    {
        var token = await tokenRepository.FirstOrDefaultAsync(token => token.User == username && token.Service == "Spotify");
        // string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
        // string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        string dataClient = $"{this.clientId}:{this.clientSecret}";
        dataClient = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataClient));
        string authorization = $"Basic {dataClient}";
        client.DefaultRequestHeaders.Add("Authorization", authorization);
        

        var newForm = new List<KeyValuePair<string, string>>();
        newForm.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
        newForm.Add(new KeyValuePair<string, string>("refresh_token", $"{token.RefreshToken}"));

        var newBody = new FormUrlEncodedContent(newForm);
        var refreshToken = await client.PostAsync("https://accounts.spotify.com/api/token", newBody);
        

        var result = await refreshToken.Content.ReadFromJsonAsync<SpotifyToken>();
        token.ServiceToken = result.access_token;
        token.LastUpdate = DateTime.Now;
        await tokenRepository.Update( token );

    }
}