
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