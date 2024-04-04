using music_api;
using music_api.DTO.all;
using music_api.DTO.spotify;
using music_api.Model;

public abstract class StreamerController : ControllerBase
{
    public abstract Task<ActionResult<StringReturn>> Get (bool debug , [FromBody] JWT data,[FromServices] IRepository<User> userRepository,[FromServices] IRepository<Token> tokenRepository,[FromServices] IJwtService jwt);
    public abstract Task<ActionResult> LogOff (bool debug , [FromBody] JWT data, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository);
    public abstract Task<ActionResult> Callback (bool debug , [FromServices] HttpClient client, [FromBody] CallbackData data, [FromServices] IRepository<Token> tokenRepository,[FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwt);
    public abstract Task<ActionResult> RefreshToken (bool debug , [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository, [FromServices] IRepository<User> userRepository, [FromServices] IJwtService jwtService, [FromBody] JWT jwt);
    public abstract Task GetMusicData (bool debug , [FromServices] HttpClient client, [FromBody] string accessToken); //! nada a ver
    public abstract Task<ActionResult> GetUserPlaylists (bool debug , [FromBody] JWTWithGetPlaylistData data, [FromServices] IJwtService jwt, [FromServices] IRepository<User> userRepository, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client);
    public abstract Task<ActionResult> GetPlaylist (bool debug , [FromQuery(Name = "id")] string id, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client);
    public abstract Task<ActionResult> GetPlaylistTracks (bool debug , [FromQuery(Name = "id")] string id, [FromQuery(Name = "streamer")] string streamer, [FromBody] JWT body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client,[FromServices] IRepository<User> userRepository );
    public abstract Task<ActionResult> GetMoreTracks(bool debug , [FromBody] JWTWithData<string> body, [FromServices] IJwtService jwt, [FromServices] IRepository<Token> tokenRepository, [FromServices] HttpClient client);
    public abstract Task<ActionResult> CreatePlaylist(bool debug , [FromBody] UserCreatePlaylistWithJwt data,[FromServices] IJwtService jwt,[FromServices] IRepository<Token> tokenRepository,[FromServices] IRepository<User> userRepository,[FromServices] HttpClient client);
    public abstract Task<ActionResult> AddTrackToPlaylist(bool debug , [FromBody] JWTWithData<TrackAndPlaylist> data,[FromServices] IJwtService jwt,[FromServices] IRepository<Token> tokenRepository,[FromServices] IRepository<User> userRepository,[FromServices] HttpClient client);
    protected abstract Task GetUserData([FromServices] HttpClient client, string token);
    protected abstract Task refreshToken(string username, [FromServices] HttpClient client, [FromServices] IRepository<Token> tokenRepository);
}