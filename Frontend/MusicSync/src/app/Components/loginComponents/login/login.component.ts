import { Component, EventEmitter, Output } from '@angular/core';
import { UserServices } from '../../../services/User.Service';
import { JwtWithData, jwt, jwtWithVerified, userLoginData } from '../../../services/UserDto';
import { Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    @Output() sendAlert = new EventEmitter();
    @Output() siginClickEvent = new EventEmitter();
    @Output() sendToVerify = new EventEmitter<jwt>();
    constructor(
        private service: UserServices,
        private router: Router
    ) { }
    userForm!: FormGroup;

    ngOnInit() {
        this.userForm = new FormGroup({
            identify: new FormControl("", [Validators.required]),
            password: new FormControl("", [Validators.required])
        })
    }

    get identify()  {
        return this.userForm.get('identify')!;
    }
    get password()  {
        return this.userForm.get('password')!;
    }

    

    siginClicked() {
        this.siginClickEvent.emit();
    }

    login() {
        var userData: userLoginData = {
            identify: this.identify.value,
            password: this.password.value
        }
        if(this.userForm.invalid)
            return

        this.service
            .Login(userData)
            .subscribe({
                next: (res: JwtWithData<boolean>) => {

                    if (!res.data) {
                        console.log(res);
                        sessionStorage.setItem('useridentify', this.identify.value);
                        this.sendToVerify.emit(res.jwt);
                        return;
                    }

                    sessionStorage.setItem('jwt', res.jwt.value);
                    this.router.navigate(['/']);
                },
                error: (err: HttpErrorResponse) => {
                    console.log(err);
                    switch (err.status) {
                        case 401:

                            this.sendAlert.emit(err.error);
                            break;
                        case 404:
                            this.sendAlert.emit(err.error);
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
