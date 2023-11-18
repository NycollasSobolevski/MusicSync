import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { StreamerService } from '../../services/Streamer.Service';
import { CallbackData } from '../../services/SpotifyDto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-callback-page',
  templateUrl: './callback-page.component.html',
  styleUrls: ['./callback-page.component.css']
})
export class CallbackPageComponent {

  constructor ( 
    private route : ActivatedRoute,
    private service : StreamerService,
    private router : Router
  ) {}

  ngOnInit ( ) {
    var streamer = "";
    this.route.queryParamMap.subscribe( query => {
      streamer = query.get('streamer') ?? "";
      
      if(streamer == "")
        return;
      if(streamer == "deezer")
        this.DeezerCallback();
      if(streamer == "spotify")
        this.SpotifyCallback();

    })
  }

  DeezerCallback(){
    console.log("DeezerCallback");
    var data: CallbackData = {
      jwt: sessionStorage.getItem('jwt')!,
      code: '',
      state: ''
    }

    this.route.queryParamMap.subscribe( query => {
      data.code = query.get('code') ?? "";

      console.log(data);
      
      this.service.Callback("Deezer",data).subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['/']);
        },
        error: (err : HttpErrorResponse) => {
          console.log(err.message);
          
        }
      });
    })
  
  }

  SpotifyCallback(){
    var data : CallbackData = {
      jwt: sessionStorage.getItem('jwt') ?? "",
      code: '',
      state: ''
    }
  

    this.route.queryParamMap.subscribe( query=> {
      data.code = query.get('code') ?? "";
      data.state = query.get('state') ?? "";

      console.log(data);
      

      this.service.Callback("Spotify",data).subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['/']);
        },
        error: (err : HttpErrorResponse) => {
          console.log(err.message);
          
        }
      });
    })
  }
}
