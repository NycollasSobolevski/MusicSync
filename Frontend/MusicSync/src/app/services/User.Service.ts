import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { JwtWithData, jwt, jwtWithVerified, userLoginData, userRegisterData } from "./UserDto";
import { LoaderService } from "./loader.service";

@Injectable({
    providedIn: 'root'
})
export class UserServices {
    
    constructor ( private http : HttpClient,
        private loaderService: LoaderService ) {  }
    
    private port = '5179'
    Login ( data : userLoginData ) {
        return this.http
            .post<jwtWithVerified>( `http://localhost:${this.port}/User/Login`, data )
            .pipe();
    };
    Register ( data : userRegisterData ) {
        return this.http
            .post<jwtWithVerified>( `http://localhost:${this.port}/User/CreateAccount`, data  )
            .pipe();
    };   
    VerifyEmail ( body : JwtWithData ) {
        return this.http
            .post<jwt>( `http://localhost:${this.port}/User/VerifyEmail`, body )
            .pipe();
    }
}