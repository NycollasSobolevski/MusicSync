import { Component } from '@angular/core';

@Component({
  selector: 'app-user-config-page',
  templateUrl: './user-config-page.component.html',
  styleUrls: ['./user-config-page.component.css']
})
export class UserConfigPageComponent {
  tab = "streamers";


  selectTab(event : string){
    console.log(event);
    this.tab = event;
  }
}
