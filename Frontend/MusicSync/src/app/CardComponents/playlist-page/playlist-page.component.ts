import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
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
  @Input() PlaylisData : itemsOfPlaylist = {
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

  constructor( private service : SpotifyService ){  }

  ngOnInit(){
    console.log(this.PlaylisData);
    this.service.GetPlaylistData(this.jwt, this.PlaylisData.id).subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (error) => {console.log(error);
      }
    });
  }
}
