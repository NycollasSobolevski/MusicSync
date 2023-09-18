import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { itemsOfPlaylist } from 'src/app/services/SpotifyDto';
import { SpotifyService } from 'src/app/services/SpotifyService';
import { IStreamerService } from 'src/app/services/StreamerService';
import { jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-playlist-page',
  templateUrl: './playlist-page.component.html',
  styleUrls: ['./playlist-page.component.css']
})
export class PlaylistPageComponent {
  protected PlaylisData : itemsOfPlaylist = {
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

  private jwt : jwt = {
    value: sessionStorage.getItem('jwt') || '',
  }

  constructor( private service : SpotifyService,
    private router : ActivatedRoute  
  ){  }

  ngOnInit(){
    var id ;
    var streamer ;
    
    this.router.queryParams.subscribe(params => {
      id       = params['id'];
      streamer = params['streamer'];      
    });
    console.log(id);
    
    this.getData(id ?? '');
  }

  getData(id : string) {
    console.log(id);
    
    this.service.GetPlaylistData(this.jwt, id).subscribe({
      next: ( data : any ) => {
        this.PlaylisData = data;
      }
    })
  }
}
