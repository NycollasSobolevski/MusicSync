import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CallbackData, NewPlaylist, NewTrack, NewTrackToPlaylist, Playlist, PlaylistsArray, StringReturn, itemsOfPlaylist } from "./SpotifyDto";
import { JWTWithGetPlaylistData, JwtWithData, jwt } from "./UserDto";
import {  IStreamerService } from "./IStreamer.Service";
import { EnvironmentsService } from "./environments.service";

@Injectable({
    providedIn: 'root'
})
export class StreamerService implements IStreamerService{
    private environment;
    private url: string | undefined;
    
    constructor (
        private http : HttpClient,
        private envService : EnvironmentsService
    ) { 
        this.environment = envService.getEnvironment();
        this.url = this.environment.BACKEND_URL + "/" ;
    }

    GetAccesUrl  ( streamer: string,  data : jwt ) {
        return this.http.post<StringReturn>( `${this.url}${streamer}/GetData`, data )
    };
    Callback  ( streamer: string,  data : CallbackData ) {
        return this.http.post(`${this.url}${streamer}/callback`, data);
    };
    GetPlaylists  ( streamer: string,  data : JWTWithGetPlaylistData ) {
        return this.http.post<PlaylistsArray>(`${this.url}${streamer}/GetUserPlaylists`, data)
    };
    GetPlaylist  (streamer: string,  data: jwt, playlistId : string) {
        return this.http.post<itemsOfPlaylist>(`${this.url}${streamer}/GetPlaylist?id=${playlistId}`, data)
    };
    GetMoreTracks ( streamer: string,  data : JwtWithData<string> ){
        return this.http.post<Playlist>(`${this.url}${streamer}/GetMoreTracks`, data)
    }
    GetTrackContent ( streamer: string,  data : JwtWithData<string> ){
        return this.http.post(`${this.url}${streamer}/GetTrackContent`, data)
    }
    GetPlaylistTracks ( streamer: string,  data : jwt, playlistId : string ) {
        return this.http.post<Playlist>(`${this.url}${streamer}/GetPlaylistTracks?id=${playlistId}&streamer=${streamer}`, data)
    }
    RefreshToken  ( streamer: string,  data : jwt ) {
        return this.http.post(`${this.url}${streamer}/RefreshToken`, data)
    };
    LogOff  ( streamer: string,  data : jwt ) {
        return this.http.post(`${this.url}${streamer}/LogOff`, data)
    };
    CreatePlaylist  ( streamer: string,  data : JwtWithData<NewPlaylist> ) {
        return this.http.post<itemsOfPlaylist>(`${this.url}${streamer}/CreatePlaylist`, data)
    };
    AddTrackToPlaylist (streamer: string, data : JwtWithData<NewTrackToPlaylist> ) {
        return this.http.post(`${this.url}${streamer}/AddTrackToPlaylist`, data)
    }
}