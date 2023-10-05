import { Component, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  constructor (
    private router : Router,
    private route : ActivatedRoute
  ) {}
  @Output() sendAlertEvent = new EventEmitter<string>();
  alertMessage = "";
  alertContainer = false;
  VerifyEmail= false;
  protected jwt : jwt = {
    value: ''
  };

  protected isLogin = true;
  
  ngOnInit(){
    
  }
  changeToSigin () {
    this.isLogin = !this.isLogin;
  }

  changeToVerifyEmail (jwt : jwt) {
    console.log(`in login page: ${jwt}`);
    
    this.VerifyEmail = !this.VerifyEmail;
    this.jwt = jwt;
  }

  alert(message : string){
    this.alertContainer = true;
    this.alertMessage = message;
    // setTimeout(() => {
    //   this.alertContainer = false;
    // }, 2000);
  }
}
