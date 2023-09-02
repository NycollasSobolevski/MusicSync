import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginPageComponent } from '../login-page/login-page.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
  jwt = sessionStorage.getItem("jwt") ?? undefined;

  constructor ( 
    private router : Router,
  ) {}

  ngOnInit () {
    if (this.jwt === undefined) {
        this.router.navigate(["Login"]);
    }
  }
}
