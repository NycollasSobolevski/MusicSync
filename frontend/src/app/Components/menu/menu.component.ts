import { Component } from '@angular/core';
import { StreamerService } from '../../services/Streamer.Service';
import { StringReturn } from '../../services/SpotifyDto';
import { ActivatedRoute, Router } from '@angular/router';
import { jwt } from '../../services/UserDto';
import { streamers } from '../../../streamers';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  spotifyCard = false;
  deezerCard = false;
  link = ''
  cardOn= false;
  ActivatedStreamerCard = "";
  streamers = streamers;
  
  private jwt : jwt = {
    value: sessionStorage.getItem('jwt') ?? ""
  }
  

  constructor(
    private service: StreamerService,
    private router: Router,
    private route : ActivatedRoute
  ) { }
  
  ngOnInit() {
    this.update();

    if(this.jwt.value == "" || this.jwt.value == undefined)
      return
    
    this.getStreamerAccess("spotify");
  }

  update () {
    this.route.queryParamMap.subscribe( qrr => {
      //todo; switch case na querry com parametro TAB para selecionar o card de stream que o usuario está vendo
      switch (qrr.get('tab')?.toLocaleLowerCase()) {
        case "":
          break;
        case "spotify":
          this.spotifyCard = true;
          this.ActivatedStreamerCard = "Spotify";
          this.cardOn = true;
          break;
        case "deezer":
          this.deezerCard = true;
          this.ActivatedStreamerCard = "Deezer";

          this.cardOn = true;
          break;
        default:
          break;
      }
    })
  }

  closeAllCards (){
    this.spotifyCard = false;
    this.deezerCard  = false;
    this.router.navigate(['/']);
    this.cardOn = false;
  }

  getStreamerAccess ( streamer : string ) {
    this.service
      .GetAccesUrl( streamer, this.jwt )
      .subscribe({
        next: (res: StringReturn) => {
          this.link = res.data
        },
        error: (err) => {
          console.log("ERRO:");
          console.log(err);
          
        }
      }
    );
  }

}
