import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { SpotifyService } from '../../services/Spotify.Service';
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
    private service : SpotifyService,
    private router : Router
  ) {}

  ngOnInit ( ) {
    this.route.queryParamMap.subscribe( query=> {
      this.data.code = query.get('code') ?? "";
      this.data.state = query.get('state') ?? "";

      console.log(this.data);
      

      this.service.Callback(this.data).subscribe({
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
