using music_api;
using music_api.DTO;
using music_api.Model;

public abstract class StreamerController : ControllerBase
{
    protected readonly string serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");
    protected readonly string frontPort = Environment.GetEnvironmentVariable("FRONTEND_PORT");

    protected readonly string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
    protected readonly string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

    protected readonly string redirectCallback = $"http://localhost:{Environment.GetEnvironmentVariable("FRONTEND_PORT")}/spotifyCallback";


    public abstract Task<ActionResult<StringReturn>> Get ([FromBody] JWT data,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt,
        [FromServices] IRepository<Token> tokenRepository);
    public abstract Task<ActionResult> LogOff ();
    public abstract Task<ActionResult> Callback ( [FromServices] HttpClient client,
        [FromBody] CallbackData data,
        [FromServices] IRepository<Token> tokenRepository,
        [FromServices] IRepository<User> userRepository,
        [FromServices] IJwtService jwt);
    public abstract Task<ActionResult> RefreshToken ();
    public abstract Task GetMusicData (); //! nada a ver
    public abstract Task<ActionResult> GetUserPlaylists ();
    public abstract Task<ActionResult> GetPlaylist ();
    public abstract Task<ActionResult> GetPlaylistTracks ();
    public abstract Task<ActionResult> GetMoreTracks();
    protected abstract Task<SpotifyUserData> GetUserSpotify();
    protected abstract Task refreshToken();
}