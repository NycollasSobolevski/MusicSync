import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { PlaylistsArray, itemsOfPlaylist } from 'src/app/services/SpotifyDto';
import { StreamerService } from 'src/app/services/Streamer.Service';
import { JWTWithGetPlaylistData, jwt } from 'src/app/services/UserDto';
import {streamers} from '../../../../streamers'

@Component({
  selector: 'app-playlist-card',
  templateUrl: './playlist-card.component.html',
  styleUrls: ['./playlist-card.component.css']
})
export class PlaylistCardComponent {

  constructor (
    private service : StreamerService,
    private sanitizer : DomSanitizer,
    private router : Router
  ) {}
  
  @Input() streamer! : string;
  @Output() closeCardEvent = new EventEmitter()

  playlists : PlaylistsArray = {
    items: []
  };

  private jwt : JWTWithGetPlaylistData = {
    jwt:{
      value: sessionStorage.getItem('jwt') ?? ""
    },
    offset: 0,
    limit: 20
  }
  
  async refreshToken(){
    this.service.RefreshToken(this.streamer,this.jwt.jwt).subscribe({
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
    this.service.GetPlaylists(this.streamer,this.jwt).subscribe({
      next: ( data : any ) => {
        if(this.playlists.items == null)
          this.playlists = data;
        else{
          this.playlists.items?.push(...data.items);
        }
        
        if(this.playlists != null)
          sessionStorage.setItem(`${this.streamer}Playlists`, JSON.stringify(this.playlists));
        return data.result;
      },
      error: error => {
        if (error.status == 401) {
          console.log("Refreshing token");
          this.service.RefreshToken(this.streamer,this.jwt.jwt).subscribe({
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

  public getStreamerIcon(){
    var path='';
    streamers.streamers.forEach(element => {

      if(element.name.toUpperCase() === this.streamer.toUpperCase()){
        path = element.path
      }
    });
    console.log(path);
    
    return path
  }

  async ngOnInit() {
    if (this.jwt.jwt.value == "") 
      return;
    
    var sessionPlaylists = sessionStorage.getItem(`${this.streamer}Playlists`) ?? "";
    console.log(sessionPlaylists);
    

    if(sessionPlaylists != ""){
      this.playlists = JSON.parse(sessionStorage.getItem(`${this.streamer}Playlists`) ?? "");
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
    this.router.navigate(['/playlist'], { queryParams: { id: playlist.id, streamer: this.streamer } });
  }

  closeCard(){
    this.closeCardEvent.emit();
  }
}
