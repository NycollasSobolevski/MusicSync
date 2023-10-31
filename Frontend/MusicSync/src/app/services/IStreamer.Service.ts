import { HttpClient } from "@angular/common/http";
import { CallbackData, NewPlaylist, NewTrack, Playlist, PlaylistsArray, StringReturn, itemsOfPlaylist } from "./SpotifyDto";
import { JWTWithGetPlaylistData, JwtWithData, jwt } from "./UserDto";
import { Observable } from "rxjs";

interface IStreamerService{
    GetAccesUrl ( streamer:string, data : jwt ) : Observable<StringReturn>;
    Callback ( streamer:string, data : CallbackData ) : Observable<Object>;
    GetPlaylists ( streamer:string, data : JWTWithGetPlaylistData ) : Observable<PlaylistsArray>;
    GetPlaylist ( streamer:string, data : jwt, playlistId : string) : Observable<Playlist>;
    GetPlaylistTracks( streamer:string, data : jwt, playlistId : string ) : Observable<Object>;
    GetMoreTracks( streamer:string, data : JwtWithData<string> ) : Observable<Object>;
    GetTrackContent( streamer:string, data : JwtWithData<string> ) : Observable<Object>;
    RefreshToken ( streamer:string, data : jwt ) : Observable<Object>;
    CreatePlaylist ( streamer: string,  data : JwtWithData<NewPlaylist> ) : Observable<itemsOfPlaylist>;
    AddTrackToPlaylist (streamer: string, data : JwtWithData<NewTrack> ) : Observable<Object>;
    LogOff ( streamer:string, data : jwt ) : Observable<Object>;
}


export { IStreamerService }
