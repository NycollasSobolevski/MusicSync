namespace music_api.DTO;

public class DeezerToken
{
    public string access_token { get; set; }
    public int expires { get; set; }
}

public record DeezerUserData
{
    public string id { get; set; }
    public string name { get; set; }
    public string lastname { get; set; }
    public string firstname { get; set; }
    public int status { get; set; }
    public string birthday { get; set; }
    public string inscription_date { get; set; }
    public string gender { get; set; }
    public string link { get; set; }
    public string picture { get; set; }
    public string picture_small { get; set; }
    public string picture_medium { get; set; }
    public string picture_big { get; set; }
    public string picture_xl { get; set; }
    public string country { get; set; }
    public string lang { get; set; }
    public bool is_kid { get; set; }
    public string explicit_content_level { get; set; }
    public List<string> explicit_content_levels_available { get; set; }
    public string tracklist { get; set; }
    public string type { get; set; }
}