import { HttpErrorResponse, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Component } from '@angular/core';
import { Playlist, itemsOfPlaylist } from 'src/app/services/SpotifyDto';
import { SpotifyService } from 'src/app/services/SpotifyService';
import { jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-spotify',
  templateUrl: './spotify.component.html',
  styleUrls: ['./spotify.component.css']
})
export class SpotifyComponent {
  constructor (
    private service : SpotifyService
  ) {}

  playlists : Playlist = {};

  private jwt : jwt = {value: sessionStorage.getItem('jwt') ?? ""}
  
  refreshed = false;
  refreshToken(){
    this.service.RefreshToken(this.jwt).subscribe({
      next: ( data ) => {
        console.log("Refreshed token");
        this.refreshed = true;
      },
      error: error => {
        console.error('There was an error!', error);
        return;
      }
    });
  }
  getPlaylist(){
    this.service.GetPlaylists(this.jwt).subscribe({
      next: ( data : any ) => {
        console.log(data.result);
        this.playlists = data.result;
      },
      error: error => {
        if (error.status == 401) {
          console.log("Refreshing token");
          this.service.RefreshToken(this.jwt).subscribe({
            next: ( data ) => {
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

  ngOnInit() {
    let res;

    if (this.jwt.value == "") 
      return;

    this.refreshToken();
    this.getPlaylist();    
  }
}
