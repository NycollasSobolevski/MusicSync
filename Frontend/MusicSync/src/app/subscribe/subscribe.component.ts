import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserServices } from '../services/UserServices';
import { userRegisterData } from '../services/UserDto';
import { EMPTY } from 'rxjs';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {
  constructor ( private router : Router, private service  : UserServices ) {}

  isAlert = false;
  alertContent = ''

  @Output() loginClickEvent = new EventEmitter();
  repassword = '';
  userData : userRegisterData = {
    name: '',
    birth: new Date,
    email: '',
    password: '',
  }

  signInClicked ( ) {
    if( !this.checkPassword() )
      return
    if(!this.checkAttributes())
      return
    console.log(this.userData);
    
    this.service
      .Register(this.userData)
      .subscribe({
        next: (next) => {
          console.log(next);
          
          this.router.navigate(['/']);
          window.location.reload();
          console.log("Subscription successfull");
          
        },
        error: (err : HttpErrorResponse) => {
          switch(err.status){
            case 401:
              this.Alert(err.message)
          }
        }
      });
  }

  checkPassword () {
    if(this.userData.password.length < 8){
      this.Alert("Password must contain special characters and be longer than 8 characters ")
      return
    }

    if (this.userData.password === this.repassword){
      return true
    }
    this.Alert("Passwords not match")
    return false
  }

  checkAttributes(){
    if(this.userData.name == ''){
      this.Alert("Usernam not inserted")
      return false;
    }
    if(this.userData.email == ''){
      this.Alert("Email not inserted")
      return false;
    }
    if(this.userData.birth == new Date()){
      this.Alert("Birth not inserted")
      return false;
    }
    if(this.userData.password == ''){
      this.Alert("Password not inserted")
      return false;
    }
    return true
  }

  Alert ( content : string ) {
    this.alertContent = content;
    this.isAlert = true
  }

  loginClicked ( ) {
    this.loginClickEvent.emit();
  }
}
