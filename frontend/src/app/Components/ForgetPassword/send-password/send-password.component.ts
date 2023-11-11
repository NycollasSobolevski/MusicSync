import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserServices } from 'src/app/services/User.Service';
import { userLoginData } from 'src/app/services/UserDto';

@Component({
  selector: 'app-send-password',
  templateUrl: './send-password.component.html',
  styleUrls: ['./send-password.component.css','../../loginComponents/login/login.component.css']
})
export class SendPasswordComponent {
  @Output() nextPage = new EventEmitter();
  @Input() email = '';
  @Input() token = '';


  constructor(private service : UserServices ) { 
    this.userForm = new FormGroup({
      password: new FormControl("", [Validators.required]),
      repassword: new FormControl("", [Validators.required]),
    })
  }
  userForm! : FormGroup;

  get password()  {
    return this.userForm.get('password')!;
  }
  get repassword()  {
    return this.userForm.get('repassword')!;
  }

  sendEmailIdentify(){
    if(this.password.value != this.repassword.value)
      return;
    const bodyData : userLoginData = {
      identify: this.email,
      password: this.password.value,
      token: this.token
    }
    this.service.UpdatePassword(bodyData).subscribe({
      next: ( data ) => {
        console.log(data);
        this.nextPage.emit("");
        sessionStorage.clear();
      }, 
      error: ( err ) => {
        console.log(err);
      }
    });
  }
}
