import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { identity } from 'rxjs';
import { UserServices } from 'src/app/services/User.Service';
import { JwtWithData, jwt, userLoginData } from 'src/app/services/UserDto';

@Component({
  selector: 'app-send-token',
  templateUrl: './send-token.component.html',
  styleUrls: ['./send-token.component.css','../../loginComponents/login/login.component.css']
})
export class SendTokenComponent {
  @Output() nextPage = new EventEmitter();
  @Input() email = '';

  constructor(private service : UserServices ) { 
    this.userForm = new FormGroup({
      token: new FormControl("", [Validators.required]),
    })
  }
  userForm! : FormGroup;

  get token()  {
    return this.userForm.get('token')!;
  }

  sendToken(){

    const body : userLoginData = {
      identify: this.email,
      password: '',
      token: this.token.value
    }
    this.service.VerifyEmail(body).subscribe({
      next : ( res ) => {
        this.nextPage.emit( `token: ${this.token.value}` );
      },
      error : ( err ) => {
        console.log(err);
      }
    });
  }
}
