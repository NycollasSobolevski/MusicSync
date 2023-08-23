
namespace music_api.Model;

public partial class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}