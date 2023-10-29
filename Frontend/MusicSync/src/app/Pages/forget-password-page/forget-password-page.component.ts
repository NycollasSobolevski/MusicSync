import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserServices } from 'src/app/services/User.Service';

@Component({
  selector: 'app-forget-password-page',
  templateUrl: './forget-password-page.component.html',
  styleUrls: ['./forget-password-page.component.css','../login-page/login-page.component.css']
})
export class ForgetPasswordPageComponent {
  page = ["identify", "token", "password"];
  index = 0;
  token = '';
  email = '';

  constructor( private route : Router ) { }

  nextPage (event : string) {
    console.log(event);
    
    if(event.includes('token')){
      this.token = event.split(':')[1];
      console.log(this.token);
    }
    if(event.includes('email')){
      this.email = event.split(':')[1];
      console.log(this.email);
    }
    if(this.index == 2)
      this.route.navigate(['/'])
    this.index ++;
    console.log(this.email +" "+ this.token);
    
  }
}
