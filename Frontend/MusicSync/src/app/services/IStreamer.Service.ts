import { HttpClient } from "@angular/common/http";
import { CallbackData, StringReturn } from "./SpotifyDto";
import { JWTWithGetPlaylistData, JwtWithData, jwt } from "./UserDto";
import { Observable } from "rxjs";

interface IStreamerService{
    GetAccesUrl ( streamer:string, data : jwt ) : Observable<StringReturn>;
    Callback ( streamer:string, data : CallbackData ) : Observable<Object>;
    GetPlaylists ( streamer:string, data : JWTWithGetPlaylistData ) : Observable<Object>;
    GetPlaylist ( streamer:string, data : jwt, playlistId : string) : Observable<Object>;
    GetPlaylistTracks( streamer:string, data : jwt, playlistId : string ) : Observable<Object>;
    GetMoreTracks( streamer:string, data : JwtWithData<string> ) : Observable<Object>;
    GetTrackContent( streamer:string, data : JwtWithData<string> ) : Observable<Object>;
    RefreshToken ( streamer:string, data : jwt ) : Observable<Object>;
    LogOff ( streamer:string, data : jwt ) : Observable<Object>;
}


export { IStreamerService }
