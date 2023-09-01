import { Component } from '@angular/core';
import { SpotifyService } from '../services/SpotifyService';
import { StringReturn } from '../services/SpotifyDto';
import { Router } from '@angular/router';
import { jwt } from '../services/UserDto';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  constructor(private service: SpotifyService, private router: Router) { }

  private jwt : jwt = {
    value: sessionStorage.getItem('jwt') ?? ""
  }

  link = ''

  ngOnInit() {

    if(this.jwt.value == "")
      return
    this.service
      .GetAccesUrl( this.jwt )
      .subscribe({
        next: (res: StringReturn) => {
          this.link = res.data
        },
        error: (err) => {
          console.log("ERRO:\n" + err);
        }
      })
  }
}
