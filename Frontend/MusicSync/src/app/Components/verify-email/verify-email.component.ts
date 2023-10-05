import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UserServices } from 'src/app/services/User.Service';
import { JwtWithData, jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.css']
})
export class VerifyEmailComponent {
  @Input() jwt : jwt = {
    value: ''
  };

  protected codeInput = '';

  constructor( 
    private userService : UserServices,
    private Router : Router
  ) { }

  verify(){
    var data : JwtWithData<string> = {
      jwt: this.jwt,
      data: this.codeInput
    };

    this.userService.VerifyEmail(data).subscribe({
      next: (res : jwt) => {
        console.log(res);
        sessionStorage.setItem('jwt', this.jwt.value);
        window.location.reload();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
