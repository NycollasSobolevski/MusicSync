
using music_api.DTO;

namespace music_api.DTO;




public class SpotifyToken
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string scope  { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; }
}


public class CallbackData
{
    public string jwt { get; set; }
    public string code { get; set; }
    public string state { get; set; }
}

public class SpotifyRequesAccessTokenBody
{
    public string GrantType { get; set; }
    public string Code { get; set; }
    public string RedirectUri { get; set; }
}

public class SpotifyRequestRefreshTokenBody {
    public string grant_type { get; set; }
    public string refresh_token { get; set; }
}

//TODO: convert json object in class

public class SpotifyUserData {
  public string country { get; set; }
  public string display_name { get; set; }
  public string email { get; set; }
  public explicit_content explicit_Content { get; set; }
  public external_urls external_urls { get; set; }
  public followers followers { get; set; }
  public string href { get; set; }
  public string id { get; set; }
  public Images[] images { get; set; }
  public string product { get; set; }
  public string type { get; set; }
  public string uri { get; set; }
}
public class explicit_content {
  public bool filter_enabled { get; set; }
  public bool filter_locked { get; set; }
}
public class external_urls {
  public string spotify { get; set; }
}
public class followers {
  public string href { get; set; }
  public int total { get; set; }
}



public class SpotifyUserPlaylists
{
    public string href { get; set; }
    public int limit { get; set; }
    public string next { get; set; }
    public int offset { get; set; }
    public string previous { get; set; }
    public int total { get; set; }
    public SpotifyUserPlaylistItems[] items { get; set; }
}
public class SpotifyUserPlaylistItems
{
  public bool collaborative { get; set; }
  public string description { get; set; }
  public external_urls external_urls { get; set; }
  public string href { get; set; }
  public string id { get; set; }
  public Images[] images { get; set; }
  public string name { get; set; }
  public SpotifyUserPlaylistOwner owner { get; set; }
  public bool @public { get; set; }
  public string snapshot_id { get; set; }
  public HrefTracks tracks { get; set; }
  public string type { get; set; }
  public string uri { get; set; }
}
public class SpotifyUserPlaylistOwner
{
  public external_urls external_urls { get; set; }
  public followers followers { get; set; }
  public string href { get; set; }
  public string id { get; set; }
  public string type { get; set; }
  public string uri { get; set; }
  public string display_name { get; set; }
}

public record CreatePlaylistData
{
	public string name { get; set; }
	public string description { get; set; }
	public bool @public { get; set; }
}
public record UserCreatePlaylistWithJwt
{
  public JWT Jwt { get; set; }
  public CreatePlaylistData Data { get; set; }
}
public record musicData
{
  public string name { get; set; }
  public string author { get; set; }
  public string uri { get; set; }
}

public record TrackAndPlaylist
{
  public musicData Track { get; set; }
  public string PlaylistId { get; set; }
}

public record artist
{
	public external_urls external_urls { get; set; }
	public string href { get; set; }
	public string id { get; set; }
	public string name { get; set; }
}
public record album
{
	public string name { get; set; }
}


public record TrackSearchItems
{
	public album album { get; set; }
	public artist[] artists { get; set; }
	public external_urls external_urls { get; set; }
	public string href { get; set; }
	public string id { get; set; }
	public string name { get; set; }
  public string uri { get; set; }
}
public record Tracks
{
	public string href { get; set; }
	public int limit { get; set; }
	public int offset { get; set; }
	public string next { get; set; }
	public string previous { get; set; }
	public int total { get; set; }
	public TrackSearchItems[] items { get; set; }
}
public record TrackSearchData
{
	public Tracks tracks { get; set; }
}
