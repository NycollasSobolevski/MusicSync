import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { itemsOfPlaylist } from '../../services/SpotifyDto';
import { SpotifyService } from '../../services/SpotifyService';
import { IStreamerService } from '../../services/StreamerService';
import { jwt } from '../../services/UserDto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-playlist-page',
  templateUrl: './playlist-page.component.html',
  styleUrls: ['./playlist-page.component.css']
})
export class PlaylistPageComponent {
  
  protected PlaylisData? : itemsOfPlaylist;
  protected Playlist :any;

  private jwt : jwt = {
    value: sessionStorage.getItem('jwt') || '',
  }

  constructor( private service : SpotifyService,
    private router : ActivatedRoute  
  ){  }

  ngOnInit(){
    var id = "";
    var streamer ;

    
    this.router.queryParams.subscribe(params => {
      id       = params['id'];
      streamer = params['streamer'];      
    });
    
    this.getPlaylistData(id);
    
    if(!this.playlistInSession(id)){
      this.getTracks(id ?? '');
    }
  }

  playlistInSession(id : string){
    var lastPlaylistOpened = sessionStorage.getItem('lastPlaylistOpened') || "";
    
    if(id == "")
      return false;
    if(lastPlaylistOpened == ""){
      return false;
    }
    var idOfSessionStorage = JSON
      .parse(lastPlaylistOpened)
      .href.split('/')[5];

    if(idOfSessionStorage != id )
      return false

    this.Playlist = JSON.parse(sessionStorage.getItem('lastPlaylistOpened') || "");
    return true;
  }

  getTracks(id : string) {
    if(!id || id == "")
      return;

    this.service.GetPlaylistTracks(this.jwt, id).subscribe({
      next: ( data : any ) => {
        this.Playlist = data;
        sessionStorage.setItem('lastPlaylistOpened', JSON.stringify(data));
      },
      error: ( error : HttpErrorResponse) => {
        switch (error.status) {
          case 401:
            this.service.RefreshToken(this.jwt).subscribe({
              next: () => {
                console.log('token refreshed');
                window.location.reload();
              },
              error: (error : HttpErrorResponse) => {
                console.log(`Refresh Token Error: \n ${error}`);
              }
            })
            break;
        
          default:
            console.log(error);
            break;
        }
      }
    })
  }

  getPlaylistData(id : string){
    this.service.GetPlaylist(this.jwt, id).subscribe({
      next:(data: any) => {
        this.PlaylisData = data;
        console.log(data);
        
      },
      error: (error : HttpErrorResponse) => {
        switch (error.status) {
          case 401:
            this.service.RefreshToken(this.jwt).subscribe({
              next: () => {
                console.log('token refreshed');
                window.location.reload();
              },
              error: (error : HttpErrorResponse) => {
                console.log(`Refresh Token Error: \n ${error}`);
              }
            })
            break;
        
          default:
            console.log(error);
            break;
        }
      }
    })
  }

}
