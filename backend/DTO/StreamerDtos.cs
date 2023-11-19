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
        [FromBody] JWTWithData<String> body ,
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

public record JWTWithData<T>
{
    public JWT Jwt { get; set; }
    public T Data { get; set; }
}

public record JWTWithGetPlaylistData
{
    public JWT Jwt { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }    
}

public record PlaylistData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool @Public { get; set; }
    public string Id { get; set; }
    public string Href { get; set; }
    public string Uri { get; set; }
    public List<Images> Images { get; set; }
    public PlaylistOwner Owner { get; set; }
    public HrefTracks TracksTotal { get; set; }

}
public class HrefTracks
{
  public string href { get; set; }
  public int total { get; set; }
}

public record PlaylistOwner
{
  public external_urls external_urls { get; set; }
  public followers followers { get; set; }
  public string href { get; set; }
  public string id { get; set; }
  public string type { get; set; }
  public string uri { get; set; }
  public string display_name { get; set; }
}

public record Images {
  public string url { get; set; }
  public int height { get; set; }
  public int width { get; set; }
}

public record PlaylistsData
{
    public List<PlaylistData> Items { get; set; }
    public int Total { get; set; }
}