import { Component, Input } from '@angular/core';
import { streamerJsonData } from 'src/app/services/SpotifyDto';
import { StreamerService } from 'src/app/services/Streamer.Service';
import { jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-menu-card',
  templateUrl: './menu-card.component.html',
  styleUrls: ['./menu-card.component.css']
})
export class MenuCardComponent {
  @Input() jsonData!: streamerJsonData;
  
  // constructor( private servie : StreamerService ) { }

  jwt : jwt = { 
    value : sessionStorage.getItem("jwt")!
  };
  url = ""

  ngOnInit(): void {
    this.Connect();
    console.log(this.url);
    
  }

  constructor( private service: StreamerService ) {}

  Connect(): void {
    if(this.jwt.value == null)
      return;
    
    this.service.GetAccesUrl(this.jsonData.name, this.jwt).subscribe({
      next: (data) => {
        this.url = data.data;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

}


