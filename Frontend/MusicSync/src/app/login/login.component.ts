import { Component, EventEmitter, Output } from '@angular/core';
import { UserServices } from '../services/UserServices';
import { jwtReturn, userLoginData } from '../services/UserDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @Output() siginClickEvent = new EventEmitter();

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
        next: (res : jwtReturn ) => {
          sessionStorage.setItem('jwt', res.value);
          this.router.navigate(['/']);
        },
        error: (err : any) => {
          console.log(err);
        }
      })
  }
}
