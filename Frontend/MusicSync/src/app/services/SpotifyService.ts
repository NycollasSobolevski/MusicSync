import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CallbackData, StringReturn } from "./SpotifyDto";

@Injectable({
    providedIn: 'root'
})
export class SpotifyService {
    constructor ( private http : HttpClient ) {  }
    private port = '5179'
    
    GetAccesUrl ( ) {
        return this.http.get<StringReturn>( `http://localhost:${this.port}/Spotify/GetSpotifyData` )
    };
    Callback ( data : CallbackData ) {
        return this.http.post(`http://localhost:${this.port}/Spotify/callback`, data);
    };
}