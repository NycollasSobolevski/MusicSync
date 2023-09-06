import { HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Component } from '@angular/core';
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

  private jwt : jwt = {value: sessionStorage.getItem('jwt') ?? ""}
  
  ngOnInit() {
    if (this.jwt.value != "") {
      this.service.GetPlaylists(this.jwt).subscribe({
        next: ( data ) => {
          //! se a resposta for nao autorizada, ja pedir redirecionamento para refreshToken
        },
        error: error => {
          console.error('There was an error!', error);
        }
      });
    }
  }
}
