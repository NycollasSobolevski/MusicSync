import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  title = 'MusicSync';

  constructor ( private router : Router ) {}

  ngOnInit(){
    this.checkIfLogin();
  }
  isLoged = false;
  viewConfigs = false
  switchViewConfigs(){
    this.viewConfigs = !this.viewConfigs;
  }

  checkIfLogin() {
    let log = sessionStorage.getItem('jwt') ?? undefined
    if(log === undefined)
      this.isLoged = false;
    else 
      this.isLoged = true;
  }
  settings(){
    this.router.navigate(["settings"]);
    this.switchViewConfigs()
  }
  toHome(){
    this.router.navigate(["/"]);
    this.switchViewConfigs()
  }

  logout(){
    sessionStorage.clear();
    localStorage.clear();
    this.router.navigate([""]);
    window.location.reload();

  }
}
