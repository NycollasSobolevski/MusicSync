import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  constructor (
    private router : Router,
    private route : ActivatedRoute
  ) {}

  protected isLogin = true;
  
  ngOnInit(){
    
  }
  changeToSigin () {
    this.isLogin = !this.isLogin;
  }
}
