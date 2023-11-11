import { Component } from '@angular/core';
import { TransferPlaylistObject, itemsOfPlaylist } from 'src/app/services/SpotifyDto';

@Component({
  selector: 'app-main-card',
  templateUrl: './main-card.component.html',
  styleUrls: ['./main-card.component.css']
})
export class MainCardComponent {
  sessions = ['select-streamer', 'select-playlist', 'playlist-settings', 'transfer'];
  index = 0;

  toStreamer = "";
  fromStreamer = "";
  fromPlaylist! : itemsOfPlaylist;
  toPlaylist!: itemsOfPlaylist;

  nextClicked( obj : TransferPlaylistObject ){
    
    if(obj.identifier == "streamer"){
      this.fromStreamer = obj.data.from;
      this.toStreamer = obj.data.to;
    }
    if(obj.identifier == "playlist"){
      this.fromPlaylist = obj.data;
    }
    if(obj.identifier == "newPlaylist")
      this.toPlaylist = obj.data;


    this.index++;
  }

  returnClicked(){
    if(this.index > 0)
      this.index--;
  }
}
