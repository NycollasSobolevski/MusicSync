import { Component } from '@angular/core';

@Component({
  selector: 'app-user-config-page',
  templateUrl: './user-config-page.component.html',
  styleUrls: ['./user-config-page.component.css']
})
export class UserConfigPageComponent {
  tab = "";


  selectTab(event : string){
    switch (event) {
      case "streamers":
        this.tab = "streamers";
        break;
    
      default:
        break;
    }
  }
}
