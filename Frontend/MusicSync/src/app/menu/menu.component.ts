import { Component } from '@angular/core';
import { SpotifyService } from '../services/SpotifyService';
import { StringReturn } from '../services/SpotifyDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  constructor ( private service : SpotifyService, private router : Router ) {  }


  link =''

  ngOnInit () {
    this.service.GetAccesUrl().subscribe({
      next : ( res : StringReturn ) => {
        this.link = res.data
      },
      error: ( err ) => {
        console.log("ERRO:\n" + err);
      }
    })
  }
}
