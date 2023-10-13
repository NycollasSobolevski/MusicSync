import { Component, Input } from '@angular/core';
import { Playlist } from 'src/app/services/SpotifyDto';
import { StreamerService } from 'src/app/services/Streamer.Service';

@Component({
  selector: 'app-select-playlist',
  templateUrl: './select-playlist.component.html',
  styleUrls: ['./select-playlist.component.css']
})
export class SelectPlaylistComponent {
  @Input() fromStreamer = "";

  Plailists! : Playlist 

  constructor( private service : StreamerService ){}

  ngOnInit() {
    if (this.fromStreamer == "Spotify") {
      // this.service = new SpotifyService();
    }
    // var sla = this.service.GetPlaylists(SpotifyService);
  }
}
