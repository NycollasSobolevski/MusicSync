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

public record Creator
{
    public long id { get; set; }
    public string name { get; set; }
    public string tracklist { get; set; }
    public string type { get; set; }
}

public record DeezerPlaylist
{
    public long id { get; set; }
    public string title { get; set; }
    public int duration { get; set; }
    public bool @public { get; set; }
    public bool is_loved_track { get; set; }
    public bool collaborative { get; set; }
    public int nb_tracks { get; set; }
    public int fans { get; set; }
    public string link { get; set; }
    public string picture { get; set; }
    public string picture_small { get; set; }
    public string picture_medium { get; set; }
    public string picture_big { get; set; }
    public string picture_xl { get; set; }
    public string checksum { get; set; }
    public string tracklist { get; set; }
    public string creation_date { get; set; }
    public string md5_image { get; set; }
    public string picture_type { get; set; }
    public int time_add { get; set; }
    public int time_mod { get; set; }
    public Creator creator { get; set; }
    public string type { get; set; }
}

public record DeezerPlaylistsData
{
    public List<DeezerPlaylist> data { get; set; }
    public int total { get; set; }
}