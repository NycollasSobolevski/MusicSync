import { Component, EventEmitter, Input, InputDecorator, Output } from '@angular/core';
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
  inputFocus = 0;


  constructor(private service : UserServices ) { 
    this.userForm = new FormGroup({
      token0: new FormControl("", [Validators.required]),
      token1: new FormControl("", [Validators.required]),
      token2: new FormControl("", [Validators.required]),
      token3: new FormControl("", [Validators.required]),
      token4: new FormControl("", [Validators.required]),
      token5: new FormControl("", [Validators.required]),
    })
  }

  userForm! : FormGroup;

  get token0()  {
    return this.userForm.get('token0')!;
  }get token1()  {
    return this.userForm.get('token1')!;
  }get token2()  {
    return this.userForm.get('token2')!;
  }get token3()  {
    return this.userForm.get('token3')!;
  }get token4()  {
    return this.userForm.get('token4')!;
  }get token5()  {
    return this.userForm.get('token5')!;
  }
  

  sendToken(){
    const _token = `${this.token0.value}${this.token1.value}${this.token2.value}${this.token3.value}${this.token4.value}${this.token5.value}`;
    
    const body : userLoginData = {
      identify: this.email,
      password: '',
      token: _token
    }
    this.service.VerifyEmail(body).subscribe({
      next : ( res ) => {
        this.nextPage.emit( `token: ${_token}` );
      },
      error : ( err ) => {
        console.log(err);
      }
    });
  }

  moveToNext(value : KeyboardEvent){
    
    if(value.key == 'Backspace'){
      if(this.inputFocus == 0){
        return;
      }  
      this.inputFocus--;
      var element = document.getElementById(`token${this.inputFocus}`);
      element?.focus();
      return;
    }
    
    if (this.inputFocus == 5) {
      return;
    }
    this.inputFocus++;
    var element = document.getElementById(`token${this.inputFocus}`);
    element?.focus();    
  }
}

