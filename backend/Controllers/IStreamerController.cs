using music_api;
using music_api.DTO;
using music_api.Model;

namespace music_api.Controllers;

public abstract class StreamerController : ControllerBase
{
    protected readonly string serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");
    protected readonly string frontPort = Environment.GetEnvironmentVariable("FRONTEND_PORT");

    protected readonly string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
    protected readonly string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

    protected readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/spotifyCallback";


    public abstract Task<ActionResult<StringReturn>> Get (
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository
    );
    public abstract Task<ActionResult> LogOff (
        [FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository
    );
    public abstract Task<ActionResult> Callback ( 
        [FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt);
    public abstract Task<ActionResult> RefreshToken (
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwtService,
        [FromBody] JWT jwt
    );
    public abstract Task GetMusicData (
        [FromServices] HttpClient client,
        [FromBody] String accessToken
    ); //! nada a ver
    public abstract Task<ActionResult> GetUserPlaylists (
        [FromBody] JWTWithGetPlaylistData data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    public abstract Task<ActionResult> GetPlaylist (
        [FromQuery(Name = "id")] string id,
        [FromBody] JWT body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    public abstract Task<ActionResult> GetPlaylistTracks ([FromQuery(Name = "id")] string id,
        [FromQuery(Name = "streamer")] string streamer,
        [FromBody] JWT body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client);
    public abstract Task<ActionResult> GetMoreTracks(
        [FromBody] JWTWithData<string> body ,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] HttpClient client
    );
    public abstract Task<ActionResult> CreatePlaylist (
        [FromBody] UserCreatePlaylistWithJwt data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] HttpClient client
    );
    public abstract Task<ActionResult> AddTrackToPlaylist(
        [FromBody] JWTWithData<TrackAndPlaylist> data,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] HttpClient client
    );
    protected abstract Task<SpotifyUserData> GetUserData(
        [FromServices] HttpClient client,
        string token);
    protected abstract Task refreshToken(
        string username,
        [FromServices] HttpClient client,
        [FromServices] IRepository<Token> tokenRepository
    );

}