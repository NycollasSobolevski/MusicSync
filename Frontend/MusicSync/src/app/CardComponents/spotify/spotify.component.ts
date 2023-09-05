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
      // this.service.GetPlaylists(this.jwt);
    }
  }
}
