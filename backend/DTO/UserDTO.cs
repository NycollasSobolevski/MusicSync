namespace music_api.DTO;

public record LoginData
{
    public string Identify { get; set; }
    public string Password { get; set; }
    public string? token { get; set; }
}

public record UserJwtData
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public record SigninData
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password  { get; set; }
    public DateTime Birth { get; set; }
}

public class StringReturn
{
    public string Data { get; set;}
}