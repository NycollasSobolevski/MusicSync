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
  @Input() plyalistDataInput : any;
  PlaylisData : itemsOfPlaylist = {
    href: '',
    id: '',
    images: [],
    name: '',
    owner: {
      external_urls: {
        spotify: ''
      },
      followers: {
        href: '',
        total: 0
      },
      href: '',
      id: '',
      type: '',
      uri: '',
      display_name: ''
    },
    public: false,
    snapshot_id: '',
    tracks: {
      href: '',
      total: 0
    },
    type: '',
    uri: ''
  };
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
    
    if(!this.playlistInSession(id))
      this.getData(id ?? '');
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

  getData(id : string) {
    if(!id || id == "")
      return;

    this.service.GetPlaylistTracks(this.jwt, id).subscribe({
      next: ( data : any ) => {
        
        this.Playlist = data;
        console.log(data);
        sessionStorage.setItem('lastPlaylistOpened', JSON.stringify(data));
        
        // console.log(this.PlaylisData);
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
}
