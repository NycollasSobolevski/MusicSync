import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { itemsOfPlaylist } from '../../services/SpotifyDto';
import { SpotifyService } from '../../services/SpotifyService';
import { IStreamerService } from '../../services/StreamerService';
import { jwt } from '../../services/UserDto';

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
        
        this.Playlist = data;
        console.log(data);
        
        console.log(this.PlaylisData);
      }
    })
  }
}
