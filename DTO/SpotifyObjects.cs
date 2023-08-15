
namespace music_api.DTO;

public class SpotifyToken
{
    public string AccesToken { get; set; }
    public string TokenType { get; set; }
    public string Scope  { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
}

public class SpotifyRequesAccessTokenBody
{
    public string GrantType { get; set; }
    public string Code { get; set; }
    public string RedirectUri { get; set; }
}