import { Component, EventEmitter, Output } from '@angular/core';
import { UserServices } from '../../services/User.Service';
import { jwt, jwtWithVerified, userLoginData } from '../../services/UserDto';
import { Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @Output() sendAlert = new EventEmitter();
  @Output() siginClickEvent = new EventEmitter();
  @Output() sendToVerify = new EventEmitter<jwt>();
  constructor ( 
    private service : UserServices,
    private router : Router
  ) {}

  userData : userLoginData = {
    identify : '',
    password : ''
  }

  siginClicked ( ) {
    this.siginClickEvent.emit();
  }

  login () {
    console.log(this.userData);
    
    this.service
      .Login(this.userData)
      .subscribe ({
        next: (res : jwtWithVerified ) => {
          
          if(!res.verified){
            console.log(res);
            
            this.sendToVerify.emit(res.jwt);
            return;
          }
          
          sessionStorage.setItem('jwt', res.jwt.value);
          this.router.navigate(['/']);
        },
        error: (err : HttpErrorResponse) => {
          switch (err.status) {
            case 401:
              this.sendAlert.emit(err.message);
              break;
            case 404:
              this.sendAlert.emit(err.message);
              break;
            default:
              console.log(err);
              break;
          }
        }
      })
  }
  // login2 () {
    
  //   this.service
  //     .Login(this.userData)
  //     .pipe(
  //       switchMap((res) => {
  //         res.jwt.value;
  //         return of(res)
  //       } )
  //     )

  // }
}
