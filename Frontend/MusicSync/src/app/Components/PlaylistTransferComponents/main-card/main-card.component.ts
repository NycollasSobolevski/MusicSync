import { Component } from '@angular/core';

@Component({
  selector: 'app-main-card',
  templateUrl: './main-card.component.html',
  styleUrls: ['./main-card.component.css']
})
export class MainCardComponent {
  sessions = ['select-streamer', 'select-playlist', 'select-songs', 'transfer'];
  index = 0;

  private toStreamer = "";
  private fromStreamer = "";

  nextClicked( obj : any ){
    console.log(obj);
    if(this.index == 0){
      this.fromStreamer = obj.from;
      this.toStreamer = obj.to;
    }

    if(this.index < this.sessions.length - 1)
      this.index++;
  }

  returnClicked(){
    if(this.index > 0)
      this.index--;
  }
}
