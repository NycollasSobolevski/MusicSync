
namespace music_api.DTO;

public class SpotifyToken
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string scope  { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; }
}
public class StringReturn
{
    public string Data { get; set;}
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
  public images[] images { get; set; }
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
public class images {
  public string url { get; set; }
  public int height { get; set; }
  public int width { get; set; }
}
