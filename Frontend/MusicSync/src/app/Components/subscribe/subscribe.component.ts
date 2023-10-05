import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserServices } from '../../services/User.Service';
import { userRegisterData } from '../../services/UserDto';
import { EMPTY } from 'rxjs';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {
  constructor ( private router : Router, private service  : UserServices ) {}

  isAlert = false;
  alertContent = ''
  @Output() sendAlert = new EventEmitter();
  @Output() loginClickEvent = new EventEmitter();
  repassword = '';
  
  accountForm!: FormGroup;
  ngOnInit(){
    this.accountForm = new FormGroup({
      name: new FormControl(""),
      email: new FormControl(""),
      birth: new FormControl(""),
      password: new FormControl("")
    });
  }

  get name (){
    return this.accountForm.get('name')!;
  }
  get email (){
    return this.accountForm.get('email')!;
  }
  get password (){
    return this.accountForm.get('password')!;
  }

  signInClicked ( ) {
    var userData : userRegisterData = {
      name: '',
      birth: new Date,
      email: '',
      password: '',
    }
    if( !this.checkPassword() )
      return
    if (this.accountForm.invalid)
      return
    console.log(userData);
    
    this.service
      .Register(userData)
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
              this.sendAlert.emit(err.error);
              break;
            case 400:
              this.sendAlert.emit(err.error)
              break;
            default:
              console.log(err);
              break;
          }
        }
      });
  }

  checkPassword () {
    if(this.password.value.length < 8){
      this.Alert("Password must contain special characters and be longer than 8 characters ")
      return
    }

    if (this.password.value.password === this.repassword){
      return true
    }
    this.Alert("Passwords not match")
    return false
  }

  // checkAttributes(){
  //   if(this.name == ''){
  //     this.Alert("Usernam not inserted")
  //     return false;
  //   }
  //   if(this.email == ''){
  //     this.Alert("Email not inserted")
  //     return false;
  //   }
  //   if(this.birth == new Date()){
  //     this.Alert("Birth not inserted")
  //     return false;
  //   }
  //   if(this.password == ''){
  //     this.Alert("Password not inserted")
  //     return false;
  //   }
  //   return true
  // }

  Alert ( content : string ) {
    this.alertContent = content;
    this.isAlert = true
  }

  loginClicked ( ) {
    this.loginClickEvent.emit();
  }
}
