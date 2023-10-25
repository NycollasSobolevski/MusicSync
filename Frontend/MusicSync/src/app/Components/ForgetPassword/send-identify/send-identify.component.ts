import { HttpResponseBase } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserServices } from 'src/app/services/User.Service';
import { userJwtData } from 'src/app/services/UserDto';

@Component({
  selector: 'app-send-identify',
  templateUrl: './send-identify.component.html',
  styleUrls: ['./send-identify.component.css', '../../loginComponents/login/login.component.css']
})
export class SendIdentifyComponent {
  @Output() nextPage = new EventEmitter();

  constructor(private service : UserServices ) { 
    this.userForm = new FormGroup({
      identify: new FormControl("", [Validators.required]),
    })
  }
  userForm! : FormGroup;

  get identify()  {
    return this.userForm.get('identify')!;
  }

  verifyEmailFormat(){
    //!todo ===============================================
  }

  sendEmailIdentify(){
    this.verifyEmailFormat();
    const bodyData : userJwtData = {
      Name: '',
      Email: this.identify.value
    }

    this.service.UpdateEmailToken(bodyData).subscribe({
      next: ( data ) => {       
        console.log(data);
        // sessionStorage.setItem('useridentify', this.identify.value);
        this.nextPage.emit( `email:${this.identify.value}` );
      },
      error: ( err ) => {
        console.log(err);
      }
    }
    )
  }

}
