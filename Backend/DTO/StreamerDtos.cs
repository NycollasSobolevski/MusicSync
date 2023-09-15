namespace music_api.DTO;

public record JWTWithData
{
    public JWT Jwt { get; set; }
    public string Data { get; set; }
}