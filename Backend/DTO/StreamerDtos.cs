using music_api.Model;

namespace music_api.DTO;

interface IStreamerController
{
    public string serverPort { get; set; }
    public string frontPort { get; set; }
    public string clientId { get; set; }
    public string clientSecret { get; set; }
    public string redirectCallback { get; set; }

    Task<ActionResult<StringReturn>> Get( [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository);
    Task<ActionResult> LogOff(
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository
    );
    Task<ActionResult> Callback(
        [FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt
    );
    Task<ActionResult> RefreshToken(
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwtService,
        [FromBody] JWT jwt
    );
    Task GetMusicData(
        [FromServices] HttpClient client,
        [FromBody] String accessToken
    );
    Task<ActionResult> GetUserPlaylists (
        [FromBody] JWTWithGetPlaylistData data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    Task<ActionResult> GetPlaylist(
        [FromQuery(Name = "id")] string id,
        [FromBody] JWT body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    Task<ActionResult> GetPlaylistTracks (
        [FromQuery(Name = "id")] string id,
        [FromQuery(Name = "streamer")] string streamer,
        [FromBody] JWT body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    Task<ActionResult> getMoreTracks (
        [FromBody] JWTWithData body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    Task<SpotifyUserData> getUserSpotify (
        [FromServices] HttpClient client,
        string token
    );
    Task refreshToken (
        string username,
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository
    );
    
}

public record JWTWithData
{
    public JWT Jwt { get; set; }
    public string Data { get; set; }
}