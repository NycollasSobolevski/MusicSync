
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson.IO;
using music_api;
using music_api.Controllers;
using music_api.DTO;
using music_api.Model;


[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class DeezerController : StreamerController
{

    private readonly string serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");
    private readonly string frontPort = Environment.GetEnvironmentVariable("FRONTEND_PORT");
    private readonly string clientUrl = Environment.GetEnvironmentVariable("DEEZER_CLIENT_URL");

    private readonly string clientId = Environment.GetEnvironmentVariable("DEEZER_CLIENT_ID");
    private readonly string clientSecret = Environment.GetEnvironmentVariable("DEEZER_CLIENT_SECRET");
    private readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/Callback?streamer=deezer";

    [HttpPost("GetData")]
    public override async Task<ActionResult<StringReturn>> Get(
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IJwtService jwt
    )
    {

        try{
            var userJwt = jwt.Validate<UserJwtData>(data.Value);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == userJwt.Name
            );

            var token = await tokenRepository.FirstOrDefaultAsync( token => 
                token.User == user.Name && token.Service == "Deezer"
            );

            if(token != null){
                return Ok(new StringReturn{
                    Data = "http://localhost:4200/?tab=Deezer"
                });
            }

            string scope = """
                basic_access, 
                manage_library,
                offline_access,
                delete_library,
                manage_community
            """;

            string deezerUriConnect = 
                $"https://connect.deezer.com/oauth/auth.php?app_id={clientId}&redirect_uri={redirectCallback}&perms={scope}";
            
            return Ok( new StringReturn{
                Data = deezerUriConnect
            });
        } catch (Exception e){
            System.Console.WriteLine(e.Message);
            return BadRequest("Unknow server error");
        }

    }


    [HttpPost("Callback")]
    public override async Task<ActionResult> Callback([FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    ){
        try{
            System.Console.WriteLine(data.code);

            var userJwt = jwt.Validate<UserJwtData>(data.jwt);
            var user = await userRepository.FirstOrDefaultAsync( user => 
                user.Name == userJwt.Name
            );

            string url = $"https://connect.deezer.com/oauth/access_token.php?app_id={this.clientId}&secret={this.clientSecret}&code={data.code}&output=json";
            System.Console.WriteLine(url);
            var response = await client.GetAsync(url);

            if(response.StatusCode != HttpStatusCode.OK){
                return BadRequest("Error getting token");
            }
            var tokenData = await response.Content.ReadFromJsonAsync<DeezerToken>();
            System.Console.WriteLine(tokenData);
            Token _token = new() 
            {
                User = user.Name,
                Service = "Deezer",
                ServiceToken = tokenData.access_token,
                ExpiresIn = tokenData.expires,
                LastUpdate = DateTime.Now
            };
            System.Console.WriteLine($"Token: {_token.ServiceToken}\nRefreshToken: {_token.RefreshToken}\nExpiresIn: {_token.ExpiresIn}\nLastUpdate: {_token.LastUpdate}");
            await tokenRepository.add(_token);

            return Ok();

        } catch (Exception e){
            System.Console.WriteLine(e);
            return BadRequest("Unknow server error");
        }
    }


    [HttpPost("GetMoreTracks")]
    public override Task<ActionResult> GetMoreTracks([FromBody] JWTWithData<string> body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetMusicData")]
    public override Task GetMusicData([FromServices] HttpClient client, [FromBody] string accessToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylist/")]
    public override Task<ActionResult> GetPlaylist([FromQuery(Name = "id")] string id, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetPlaylistTracks/")]
    public override Task<ActionResult> GetPlaylistTracks([FromQuery(Name = "id")] string id, [FromQuery(Name = "streamer")] string streamer, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetUserPlaylists")]
    public override Task<ActionResult> GetUserPlaylists([FromBody] JWTWithGetPlaylistData data, [FromServices] IJwtService jwt, [FromServices] IRepository<User> userRepository, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("logoff")]

    public override Task<ActionResult> LogOff([FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository)
    {
        throw new NotImplementedException();
    }

    [HttpPost("RefreshToken")]
    public override Task<ActionResult> RefreshToken([FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwtService, [FromBody] JWT jwt)
    {
        throw new NotImplementedException();
    }

    protected override Task<SpotifyUserData> GetUserData([FromServices] HttpClient client, string token)
    {
        throw new NotImplementedException();
    }

    protected override Task refreshToken(string username, [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository)
    {
        throw new NotImplementedException();
    }
}