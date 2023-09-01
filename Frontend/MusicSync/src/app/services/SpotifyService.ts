import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CallbackData, StringReturn } from "./SpotifyDto";
import { jwt } from "./UserDto";

@Injectable({
    providedIn: 'root'
})
export class SpotifyService {
    constructor ( private http : HttpClient ) {  }
    private port = '5179'
    
    GetAccesUrl ( data : jwt ) {
        return this.http.post<StringReturn>( `http://localhost:${this.port}/Spotify/GetSpotifyData`, data )
    };
    Callback ( data : CallbackData ) {
        return this.http.post(`http://localhost:${this.port}/Spotify/callback`, data);
    };
}