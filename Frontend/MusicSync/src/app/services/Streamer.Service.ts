import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CallbackData, StringReturn } from "./SpotifyDto";
import { JWTWithGetPlaylistData, JwtWithData, jwt } from "./UserDto";
import {  IStreamerService } from "./IStreamer.Service";
import { environment } from "../Environments/Environment.prod";

@Injectable({
    providedIn: 'root'
})
export class StreamerService implements IStreamerService{
    private url: string | undefined;
    private endpoint = 'Spotify'
    
    constructor (private http : HttpClient) { 
        this.url = environment.BACKEND_URL + "/" ;
    }

    GetAccesUrl  ( streamer: string,  data : jwt ) {
        return this.http.post<StringReturn>( `${this.url}${streamer}/GetSpotifyData`, data )
    };
    Callback  ( streamer: string,  data : CallbackData ) {
        return this.http.post(`${this.url}${streamer}/callback`, data);
    };
    GetPlaylists  ( streamer: string,  data : JWTWithGetPlaylistData ) {
        return this.http.post(`${this.url}${streamer}/GetUserPlaylists`, data)
    };
    GetPlaylist  (streamer: string,  data: jwt, playlistId : string) {
        return this.http.post(`${this.url}${streamer}/GetPlaylist?id=${playlistId}`, data)
    };
    GetMoreTracks ( streamer: string,  data : JwtWithData<string> ){
        return this.http.post(`${this.url}${streamer}/GetMoreTracks`, data)
    }
    GetTrackContent ( streamer: string,  data : JwtWithData<string> ){
        return this.http.post(`${this.url}${streamer}/GetTrackContent`, data)
    }
    GetPlaylistTracks ( streamer: string,  data : jwt, playlistId : string ) {
        return this.http.post(`${this.url}${streamer}/GetPlaylistTracks?id=${playlistId}&streamer=spotify`, data)
    }
    RefreshToken  ( streamer: string,  data : jwt ) {
        return this.http.post(`${this.url}${streamer}/RefreshToken`, data)
    };
    LogOff  ( streamer: string,  data : jwt ) {
        return this.http.post(`${this.url}${streamer}/LogOff`, data)
    };
}