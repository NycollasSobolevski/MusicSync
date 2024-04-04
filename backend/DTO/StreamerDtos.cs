using Microsoft.Extensions.Diagnostics.HealthChecks;
using music_api.Model;

namespace music_api.DTO.all;
using  music_api.DTO.spotify;

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
//   public followers followers { get; set; }
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


public record PlaylistTracksData
{
    public string href { get; set; }
    public List<Tracks> items { get; set; }
    public int limit { get; set; }
    public string next { get; set; }
    public int offset { get; set; }
    public string previous { get; set; }
    public int total { get; set; }
}

public record Tracks
{
    public string added_at { get; set; }
    public PlaylistOwner added_by { get; set; }
    public bool is_local { get; set; }
    public string? primary_color { get; set; }
    public Track track { get; set; }
}

// public record Track
// {
//     public string id { get; set; }
//     public string href { get; set; }
//     public string name { get; set; }
//     public bool track { get; set; }
//     public int track_number { get; set; }
//     public Album album { get; set; }
//     public List<Artist> artists { get; set; }
//     public external_urls external_Urls { get; set; }

// }
public record Artist
{
    public ExternalUrls ExternalUrls { get; set; }
    public string Href { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
}



//---------------------------------------------------------------------------------
public class SpotifyPlaylistTracksResponse
{
    public string Href { get; set; }
    public IEnumerable<Item> Items { get; set; }
    public int Limit { get; set; }
    public string Next { get; set; }
    public int Offset { get; set; }
    public object Previous { get; set; }
    public int Total { get; set; }
}

public class Item
{
    public DateTime AddedAt { get; set; }
    public AddedBy AddedBy { get; set; }
    public bool IsLocal { get; set; }
    public object PrimaryColor { get; set; }
    public Track Track { get; set; }
    public VideoThumbnail VideoThumbnail { get; set; }
}

public class AddedBy
{
    public ExternalUrls ExternalUrls { get; set; }
    public string Href { get; set; }
    public string Id { get; set; }
    public string Type { get; set; }
    public string Uri { get; set; }
}

public class ExternalUrls
{
    public string Spotify { get; set; }
}

public class Track
{
    public string PreviewUrl { get; set; }
    public List<object> AvailableMarkets { get; set; }
    public bool Explicit { get; set; }
    public string Type { get; set; }
    public bool Episode { get; set; }
    public bool IsTrack { get; set; }
    public Album Album { get; set; }
    public List<Artist> Artists { get; set; }
    public int DiscNumber { get; set; }
    public int TrackNumber { get; set; }
    public int DurationMs { get; set; }
    public ExternalIds ExternalIds { get; set; }
    public ExternalUrls ExternalUrls { get; set; }
    public string Href { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public int Popularity { get; set; }
    public string Uri { get; set; }
    public bool IsLocal { get; set; }
}

public class Album
{
    public List<string> AvailableMarkets { get; set; }
    public string Type { get; set; }
    public string AlbumType { get; set; }
    public string Href { get; set; }
    public string Id { get; set; }
    public List<Image> Images { get; set; }
    public string Name { get; set; }
    public string ReleaseDate { get; set; }
    public string ReleaseDatePrecision { get; set; }
    public string Uri { get; set; }
    public List<Artist> Artists { get; set; }
    public ExternalUrls ExternalUrls { get; set; }
    public int TotalTracks { get; set; }
}

public class Image
{
    public int Height { get; set; }
    public string Url { get; set; }
    public int Width { get; set; }
}

public class ExternalIds
{
    public string Isrc { get; set; }
}

public class VideoThumbnail
{
    public object Url { get; set; }
}
