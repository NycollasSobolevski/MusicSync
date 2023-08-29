namespace music_api.DTO;

public record LoginData
{
    public string Identify { get; set; }
    public string Password { get; set; }
}

public record ReturnLoginData
{
    public string Name { get; set; }
    public string Email { get; set; }
}