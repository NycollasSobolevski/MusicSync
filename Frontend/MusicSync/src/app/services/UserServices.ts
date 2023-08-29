import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { jwtReturn, userLoginData, userRegisterData } from "./UserDto";

@Injectable({
    providedIn: 'root'
})
export class UserServices {
    constructor ( private http : HttpClient ) {  }
    private port = '5179'
    Login ( data : userLoginData ) {
        return this.http.post<jwtReturn>( `http://localhost:${this.port}/User/Login`, data )
    };
    Register ( data : userRegisterData ) {
        return this.http.post<jwtReturn>( `http://localhost:${this.port}/User/CreateAccount`, data  )
    };   
}