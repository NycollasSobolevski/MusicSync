import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PlaylistsArray, TransferPlaylistObject } from 'src/app/services/SpotifyDto';
import { StreamerService } from 'src/app/services/Streamer.Service';
import { JWTWithGetPlaylistData, JwtWithData } from 'src/app/services/UserDto';

@Component({
  selector: 'app-select-playlist',
  templateUrl: './select-playlist.component.html',
  styleUrls: ['./select-playlist.component.css', '../main-card/main-card.component.css']
})
export class SelectPlaylistComponent {
  
  @Input() fromStreamer = "";
  Playlists! : PlaylistsArray 
  playlistForm! : FormGroup;
  @Output() nextClicked = new EventEmitter();

  constructor( private service : StreamerService ){}

  get SelectedPlaylist(){
    return this.playlistForm.get('playlist');
  }
  ngOnInit() {
    this.playlistForm = new FormGroup({
      playlist: new FormControl("",[Validators.required])
    });
        
    var data : JWTWithGetPlaylistData = {
      jwt: {
        value: sessionStorage.getItem("jwt")!,
      },
      offset: 0,
      limit: 0
    }

    var palylists = sessionStorage.getItem("SpotifyPlaylists") ?? "";
    if(palylists == "")
      this.service.GetPlaylists(this.fromStreamer, data).subscribe({
        next: (result) => {
          console.log(result);
          this.Playlists = result;
        },
        error: (error) => {
          console.log(error);
        }
      });
    
    else
      this.Playlists = JSON.parse(palylists);
  }

  next(){
    if(this.playlistForm.invalid){
      return;
    }

    const data = this.Playlists.items.find( (playlist) => 
      playlist.id == this.SelectedPlaylist?.value
    );

    const obj : TransferPlaylistObject= {
      identifier: "playlist",
      data: data
    }
    this.nextClicked.emit(obj);
  }

}
