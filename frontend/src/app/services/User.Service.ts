import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { JwtWithData, jwt, jwtWithVerified, userJwtData, userLoginData, userRegisterData } from "./UserDto";
import { LoaderService } from "./loader.service";

@Injectable({
    providedIn: 'root'
})
export class UserServices {
    
    constructor ( private http : HttpClient,
        private loaderService: LoaderService ) {  }
    
    private port = '5179'
    Login ( body : userLoginData ) {
        return this.http
            .post<JwtWithData<boolean>>( `http://localhost:${this.port}/User/Login`, body )
            .pipe();
    };
    Register ( body : userRegisterData ) {
        return this.http
            .post<jwtWithVerified>( `http://localhost:${this.port}/User/CreateAccount`, body  )
            .pipe();
    };   
    VerifyEmail ( body : userLoginData ) {
        return this.http
            .post<jwt>( `http://localhost:${this.port}/User/VerifyEmail`, body )
            .pipe();
    }
    UpdateEmailToken( body: userJwtData ) {
        return this.http
            .post( `http://localhost:${this.port}/User/UpdateEmailToken`, body )
    }
    UpdatePassword ( body : userLoginData) {
        return this.http
            .post( `http://localhost:${this.port}/User/UpdatePassword`, body )
            .pipe();
    }
}