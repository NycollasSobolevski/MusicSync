using System.Text.Json.Serialization;

namespace music_api.DTO.deezer;

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

// public record Creator
// {
//     public long id { get; set; }
//     public string name { get; set; }
//     public string tracklist { get; set; }
//     public string type { get; set; }
// }

// public record DeezerPlaylist
// {
//     public long id { get; set; }
//     public string title { get; set; }
//     public int duration { get; set; }
//     public bool @public { get; set; }
//     public bool is_loved_track { get; set; }
//     public bool collaborative { get; set; }
//     public int nb_tracks { get; set; }
//     public int fans { get; set; }
//     public string link { get; set; }
//     public string picture { get; set; }
//     public string picture_small { get; set; }
//     public string picture_medium { get; set; }
//     public string picture_big { get; set; }
//     public string picture_xl { get; set; }
//     public string checksum { get; set; }
//     public string tracklist { get; set; }
//     public string creation_date { get; set; }
//     public string md5_image { get; set; }
//     public string picture_type { get; set; }
//     public int time_add { get; set; }
//     public int time_mod { get; set; }
//     public Creator creator { get; set; }
//     public string type { get; set; }
    
// }

public record DeezerPlaylistsData
{
    public List<DeezerPlaylist> data { get; set; }
    public int total { get; set; }
}

public record DeezerArtist
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

public record DeezerAlbum
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
    public DeezerArtist artist {get;set;} //: {},
    public DeezerAlbum album {get;set;} //: {},
    public string type {get;set;} //: "track"
}

public record PlaylistCreateReturn
{
    public ulong? id {get;set;}
}


//-------------------------------------------------------------------------------------

public class DeezerPlaylist
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public bool Public { get; set; }
    public bool IsLovedTrack { get; set; }
    public bool Collaborative { get; set; }
    public int NbTracks { get; set; }
    public int Fans { get; set; }
    public string Link { get; set; }
    public string Share { get; set; }
    public string Picture { get; set; }
    public string PictureSmall { get; set; }
    public string PictureMedium { get; set; }
    public string PictureBig { get; set; }
    public string PictureXl { get; set; }
    public string Checksum { get; set; }
    public string Tracklist { get; set; }
    public string CreationDate { get; set; }
    public string Md5Image { get; set; }
    public string PictureType { get; set; }
    public Creator Creator { get; set; }
    public string Type { get; set; }
    public Tracks Tracks { get; set; }
}

public class Creator
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Tracklist { get; set; }
    public string Type { get; set; }
}

public class Tracks
{
    public List<Track> Data { get; set; }
    public string Checksum { get; set; }
}

public class Track
{
    public long Id { get; set; }
    public bool Readable { get; set; }
    public string Title { get; set; }
    public string TitleShort { get; set; }
    public string TitleVersion { get; set; }
    public string Link { get; set; }
    public int Duration { get; set; }
    public int Rank { get; set; }
    public bool ExplicitLyrics { get; set; }
    public int ExplicitContentLyrics { get; set; }
    public int ExplicitContentCover { get; set; }
    public string Preview { get; set; }
    public string Md5Image { get; set; }
    public long TimeAdd { get; set; }
    public Artist Artist { get; set; }
    public Album Album { get; set; }
    public string Type { get; set; }
}

public class Artist
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string Tracklist { get; set; }
    public string Type { get; set; }
}

public class Album
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Cover { get; set; }
    public string CoverSmall { get; set; }
    public string CoverMedium { get; set; }
    public string CoverBig { get; set; }
    public string CoverXl { get; set; }
    public string Md5Image { get; set; }
    public string Tracklist { get; set; }
    public string Type { get; set; }
}
