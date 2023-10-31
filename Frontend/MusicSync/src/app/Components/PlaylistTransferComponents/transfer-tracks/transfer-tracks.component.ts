import { Component, Input } from '@angular/core';
import { PlaylistsArray } from 'src/app/services/SpotifyDto';
import { StreamerService } from 'src/app/services/Streamer.Service';

@Component({
  selector: 'app-transfer-tracks',
  templateUrl: './transfer-tracks.component.html',
  styleUrls: ['./transfer-tracks.component.css']
})
export class TransferTracksComponent {
  @Input() toStreamer!: string;
  @Input() fromStreamer!: string;
  @Input() newPlaylist!: PlaylistsArray;
  @Input() oldPlaylist!: PlaylistsArray;


  constructor( private service : StreamerService ) { }


  addTrack(  ){

  }
}
