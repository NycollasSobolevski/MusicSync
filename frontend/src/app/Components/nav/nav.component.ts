import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NavbarService } from 'src/app/services/navbar.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnDestroy {
  title = 'MusicSync';
  show: boolean = true;
  subscription: Subscription;

  constructor ( private router : Router, private navbarService : NavbarService ) {
    this.subscription = this.navbarService.showNavbar.subscribe((value) => {
      this.show = value;

    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

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
