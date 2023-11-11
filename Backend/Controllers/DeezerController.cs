
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository
    )
    {



        string scope = """
            basic_access, 
            manage_library,
            delete_library,
            manage_community
        """;

        string deezerUriConnect = 
            $"https://connect.deezer.com/oauth/auth.php?app_id={clientId}&redirect_uri={redirectCallback}&perms={scope}";
        
        return Ok( new StringReturn{
            Data = deezerUriConnect
        });
    }


    [HttpPost("Callback")]
    public override async Task<ActionResult> Callback([FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    ){
        throw new NotImplementedException();
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