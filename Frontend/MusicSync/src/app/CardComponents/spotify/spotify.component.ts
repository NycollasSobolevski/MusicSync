import { HttpErrorResponse, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Playlist, itemsOfPlaylist } from 'src/app/services/SpotifyDto';
import { SpotifyService } from 'src/app/services/SpotifyService';
import { JWTWithGetPlaylistData, jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-spotify',
  templateUrl: './spotify.component.html',
  styleUrls: ['./spotify.component.css']
})
export class SpotifyComponent {
  constructor (
    private service : SpotifyService,
    private sanitizer : DomSanitizer,
    private router : Router
  ) {}
  // !TODO: Implementar o closeCard
    @Output() closeCardEvent = new EventEmitter()


  private jwt : JWTWithGetPlaylistData = {
    jwt:{
      value: sessionStorage.getItem('jwt') ?? ""
    },
    offset: 0,
    limit: 20
  }
  
  async refreshToken(){
    this.service.RefreshToken(this.jwt.jwt).subscribe({
      next: ( data ) => {
        console.log("Refreshed token");
      },
      error: error => {
        console.error('There was an error!', error);
        return;
      }
    });
  }
  async getPlaylist(){
    
    await this.refreshToken();

    this.service.GetPlaylists(this.jwt).subscribe({
      next: ( data : any ) => {
        if(this.playlists.items == null)
          this.playlists = data;
        else{
          this.playlists.items?.push(...data.items);
        }
        
        if(this.playlists != null)
          sessionStorage.setItem('SpotifyPlaylists', JSON.stringify(this.playlists));
        return data.result;
      },
      error: error => {
        if (error.status == 401) {
          console.log("Refreshing token");
          this.service.RefreshToken(this.jwt.jwt).subscribe({
            next: ( data ) => {
              console.log("Refreshed token");
              location.reload();
            },
            error: error => {
              console.error('There was an error!', error);
              return;
            }
          });
        }
        else
          console.error('There was an error!', error);
      }
    }
  );
  }

  async ngOnInit() {
    if (this.jwt.jwt.value == "") 
      return;
    
    var sessionPlaylists = sessionStorage.getItem('SpotifyPlaylists') ?? "";
    console.log(sessionPlaylists);
    

    if(sessionPlaylists != ""){
      this.playlists = JSON.parse(sessionStorage.getItem('SpotifyPlaylists') ?? "");
      return;
    }
    console.log("Getting playlists");
    
    await this.getPlaylist();

    // console.log(this.playlists.items[0].images[0].url)
  }

  sanitizerUrl(urlImage : string){
    return this.sanitizer.bypassSecurityTrustUrl(urlImage);
  }

  checkPlaylists(){
    this.playlists.items?.length == 0 ? true : false;
  }

  morePlaylists(){
    this.jwt.offset += 20;
    this.getPlaylist();
  }

  getPlaylistTracks(playlist : itemsOfPlaylist){
    this.router.navigate(['/playlist'], { queryParams: { id: playlist.id, streamer: "spotify" } });
  }

  playlists : Playlist = {
    items: []
  };


  closeCard(){
    this.closeCardEvent.emit();
  }
}
