import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CallbackData, StringReturn } from "./SpotifyDto";
import { JWTWithGetPlaylistData, JwtWithData, jwt } from "./UserDto";
import {  IStreamerService } from "./StreamerService";
import { environment } from "../Environments/Environment.prod";

@Injectable({
    providedIn: 'root'
})
export class SpotifyService {
    private url: string | undefined;
    private endpoint = 'Spotify'
    
    constructor (private http : HttpClient) { 
        this.url = environment.BACKEND_URL + "/" +this.endpoint;
    }

    GetAccesUrl ( data : jwt ) {
        return this.http.post<StringReturn>( `${this.url}/GetSpotifyData`, data )
    };
    Callback ( data : CallbackData ) {
        return this.http.post(`${this.url}/callback`, data);
    };
    GetPlaylists ( data : JWTWithGetPlaylistData ) {
        return this.http.post(`${this.url}/GetUserPlaylists`, data)
    };
    GetPlaylist (data: jwt, playlistId : string) {
        return this.http.post(`${this.url}/GetPlaylist?id=${playlistId}`, data)
    };
    GetMoreTracks( data : JwtWithData ){
        return this.http.post(`${this.url}/GetMoreTracks`, data)
    }
    GetPlaylistTracks( data : jwt, playlistId : string ) {
        return this.http.post(`${this.url}/GetPlaylistTracks?id=${playlistId}&streamer=spotify`, data)
    }
    RefreshToken ( data : jwt ) {
        return this.http.post(`${this.url}/RefreshToken`, data)
    };
    LogOff ( data : jwt ) {
        return this.http.post(`${this.url}/LogOff`, data)
    };
}