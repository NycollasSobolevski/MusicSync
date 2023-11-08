using music_api.DTO;
using music_api.Model;

namespace music_api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("MainPolicy")]
public class YoutubeController : StreamerController
{
        [HttpPost("AddTrackToPlaylist")]
    public override Task<ActionResult> AddTrackToPlaylist([FromBody] JWTWithData<TrackAndPlaylist> data, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("callback")]
    public override Task<ActionResult> Callback([FromServices] HttpClient client, [FromBody] CallbackData data, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt)
    {
        throw new NotImplementedException();
    }

    [HttpPost("CreatePlaylist")]
    public override Task<ActionResult> CreatePlaylist([FromBody] UserCreatePlaylistWithJwt data, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] HttpClient client)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetDeezerData")]
    public override Task<ActionResult<StringReturn>> Get([FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository)
    {
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