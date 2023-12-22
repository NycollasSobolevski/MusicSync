using System.Text.Json.Serialization;

namespace music_api.DTO;

public class DeezerToken
{
    public string access_token { get; set; }
    public int expires { get; set; }
}

public class DeezerUserData
{
    [JsonPropertyName("id")]
    public ulong? id { get; set; }
	public string name { get; set; }
	public string lastname { get; set; }
	public string firstname { get; set; }
	public int? status { get; set; }
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
	public string[] explicit_content_levels_available { get; set; }
	public string tracklist { get; set; }
	public string typ { get; set; }
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

public record Artist
{
    public ulong? id {get;set;}
    public string name {get;set;}
    public string link {get;set;}
    public string picture {get;set;}
    public string picture_small {get;set;}
    public string picture_medium {get;set;}
    public string picture_big {get;set;}
    public string picture_xl {get;set;}
    public string tracklist {get;set;}
    public string type {get;set;}
}

public record Album
{
    public ulong? id {get;set;}
    public string title {get;set;}
    public string cover {get;set;}
    public string cover_small {get;set;}
    public string cover_medium {get;set;}
    public string cover_big {get;set;}
    public string cover_xl {get;set;}
    public string md5_image {get;set;}
    public string tracklist {get;set;}
    public string type {get;set;}
}

public record SearchData
{
    public SearchTrackData[] data {get;set;}
    public int total {get;set;}
    public string next {get;set;}
}

public record SearchTrackData
{
    public ulong? id {get;set;} //: string,
    public bool readable {get;set;} //: bool,
    public string title {get;set;} //: string,
    public string title_short {get;set;} //: string,
    public string title_version {get;set;} //:string ,
    public string link {get;set;} //: string,
    public int duration {get;set;} //: string
    public long rank {get;set;} //: string,
    public bool explicit_lyrics {get;set;} //: bool,
    public int explicit_content_lyrics {get;set;} //: int,
    public int explicit_content_cover {get;set;} //: int,
    public string preview {get;set;} //: string,
    public string md5_image {get;set;} //: string,
    public Artist artist {get;set;} //: {},
    public Album album {get;set;} //: {},
    public string type {get;set;} //: "track"
}

public record PlaylistCreateReturn
{
    public ulong? id {get;set;}
}


