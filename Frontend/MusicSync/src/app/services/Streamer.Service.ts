import { HttpClient } from "@angular/common/http";
import { CallbackData, StringReturn } from "./SpotifyDto";
import { JWTWithGetPlaylistData, JwtWithData, jwt } from "./UserDto";
import { Observable } from "rxjs";

interface IStreamerService {
    GetAccesUrl ( data : jwt ) : Observable<StringReturn>;
    Callback ( data : CallbackData ) : Observable<Object>;
    GetPlaylists ( data : JWTWithGetPlaylistData ) : Observable<Object>;
    GetPlaylist (data: jwt, playlistId : string) : Observable<Object>;
    GetPlaylistTracks( data : jwt, playlistId : string ) : Observable<Object>;
    GetMoreTracks( data : JwtWithData<string> ) : Observable<Object>;
    GetTrackContent( data : JwtWithData<string> ) : Observable<Object>;
    RefreshToken ( data : jwt ) : Observable<Object>;
    LogOff ( data : jwt ) : Observable<Object>;

}


export { IStreamerService }
