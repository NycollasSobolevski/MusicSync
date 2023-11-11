import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Playlist, itemsOfPlaylist } from '../../services/SpotifyDto';
import { StreamerService } from '../../services/Streamer.Service';
import { IStreamerService } from '../../services/IStreamer.Service';
import { JwtWithData, jwt } from '../../services/UserDto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-playlist-page',
  templateUrl: './playlist-page.component.html',
  styleUrls: ['./playlist-page.component.css']
})
export class PlaylistPageComponent {
  
  protected PlaylisData? : itemsOfPlaylist;
  protected Playlist! : Playlist;

  private jwt : jwt = {
    value: sessionStorage.getItem('jwt') || '',
  }

  constructor( private service : StreamerService,
    private activatedRoute : ActivatedRoute ,
    private router : Router
  ){  }

  ngOnInit(){
    var id = "";
    var streamer ;

    
    this.activatedRoute.queryParams.subscribe(params => {
      id       = params['id'];
      streamer = params['streamer'];      
    });
    
    this.getPlaylistData(id);
    
    if(!this.playlistInSession(id)){
      this.getTracks(id ?? '');
    }
  }

  private playlistInSession(id : string){
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
  
  /**
   * Get data of the playlist if it is not in the session storage
   * @param id id of the playlist
  */
  getPlaylistData(id : string){
    var lastData = sessionStorage.getItem('lastPlaylistData') || "";
    if(lastData != ""){
      var data = JSON.parse(lastData);
      if(data.id == id){
        this.PlaylisData = data;
        return;
      }
    }


    this.service.GetPlaylist("Spotify",this.jwt, id).subscribe({
      next:(data: any) => {
        this.PlaylisData = data;
        sessionStorage.setItem('lastPlaylistData', JSON.stringify(data));
      },
      error: (error : HttpErrorResponse) => {
        switch (error.status) {
          case 401:
            this.service.RefreshToken("Spotify",this.jwt).subscribe({
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
            console.error(error);
            break;
        }
      }
    })
  }

  /**
   * Get tracks of the playlist if it is not in the session storage
   * @param id {string}
  */
  getTracks(id : string) {
    if(!id || id == "")
      return;

    this.service.GetPlaylistTracks("Spotify",this.jwt, id).subscribe({
      next: ( data : any ) => {
        this.Playlist = data;
        console.log("playlist data");
        
        console.log(data);
        
        sessionStorage.setItem('lastPlaylistOpened', JSON.stringify(data));
      },
      error: ( error : HttpErrorResponse) => {
        switch (error.status) {
          case 401:
            this.service.RefreshToken("Spotify",this.jwt).subscribe({
              next: () => {
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
  /**
   * Get more tracks of the playlist and add to This.Playlist
   * 
  */
  getMoreTrack(){
    console.log(this.Playlist.next);
    

    const body : JwtWithData<string> = {
      jwt: this.jwt,
      data: this.Playlist.next!
    }

    this.service.GetMoreTracks("Spotify",body).subscribe({
      next:(data: any) => {
        
        // ! AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        this.Playlist.items.push(...data.items);
        this.Playlist.next = data.next ?? "";
        sessionStorage.setItem('lastPlaylistOpened', JSON.stringify(this.Playlist));
      },
      error: (error : HttpErrorResponse) => {
        console.log(error);
        
      }
    })
  }

  getMusicPage(music: any){
    this.router.navigate(['/music'], { queryParams: { id: music.track.id, streamer : "spotify" } });
  }
}
