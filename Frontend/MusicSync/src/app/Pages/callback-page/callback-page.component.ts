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
  data : CallbackData = {
    jwt: sessionStorage.getItem('jwt') ?? "",
    code: '',
    state: ''
  }
  
  constructor ( 
    private route : ActivatedRoute,
    private service : StreamerService,
    private router : Router
  ) {}

  ngOnInit ( ) {
    this.route.queryParamMap.subscribe( query=> {
      this.data.code = query.get('code') ?? "";
      this.data.state = query.get('state') ?? "";

      console.log(this.data);
      

      this.service.Callback("Spotify",this.data).subscribe({
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
