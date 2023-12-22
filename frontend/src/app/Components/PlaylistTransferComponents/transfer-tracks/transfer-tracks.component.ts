import { Component, Input } from '@angular/core';
import { NewTrack, NewTrackToPlaylist, Playlist, PlaylistsArray, itemsOfPlaylist } from 'src/app/services/SpotifyDto';
import { StreamerService } from 'src/app/services/Streamer.Service';
import { JWTWithGetPlaylistData, JwtWithData, jwt } from 'src/app/services/UserDto';

@Component({
  selector: 'app-transfer-tracks',
  templateUrl: './transfer-tracks.component.html',
  styleUrls: ['./transfer-tracks.component.css']
})
export class TransferTracksComponent {
  @Input() toStreamer!: string;
  @Input() fromStreamer!: string;
  @Input() newPlaylist!: itemsOfPlaylist;
  @Input() oldPlaylist!: itemsOfPlaylist;
  oldPlaylistWithTracks! : Playlist;
  index = 0;
  jwt : jwt = {
    value: sessionStorage.getItem("jwt")!
  }


  constructor( private service : StreamerService ) { }

  ngOnInit() {
    this.getTracks(); 
    console.log(this.oldPlaylistWithTracks);
    
    const musicCount = this.oldPlaylistWithTracks.items.length;
    const allTracksCount = this.oldPlaylistWithTracks.total!;
    
  }

  transfer(){
    const musicCount = this.oldPlaylistWithTracks.items.length;
    const allTracksCount = this.oldPlaylistWithTracks.total!;
    console.log(`${musicCount} / ${allTracksCount}`);
    this.addTrack();
  }

  getMoreTacks() {
    const body: JwtWithData<string> = {
      jwt: this.jwt,
      data: this.oldPlaylistWithTracks.next!
    }
    const musicCount = this.oldPlaylistWithTracks.items.length;
    const allTracksCount = this.oldPlaylistWithTracks.total!;
    
    this.service.GetMoreTracks(this.fromStreamer, body)
    .subscribe({
      next: (res) => {
        this.oldPlaylistWithTracks.items.push(...res.items);
        this.oldPlaylistWithTracks.next = res.next;
        if(musicCount < allTracksCount){
          this.getMoreTacks();
        }
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

  getTracks(){
    if(!this.jwt.value)
      return;

    this.service.GetPlaylistTracks(this.fromStreamer, this.jwt, this.oldPlaylist.id)
      .subscribe({
        next: (res) => {
          this.oldPlaylistWithTracks = res;
          if(this.oldPlaylistWithTracks.items.length < this.oldPlaylistWithTracks.total!)
            this.getMoreTacks();
        },
        error: (err) => {
          console.log(err);
        }
      })
  }

  addTrack(  ){
    const body : JwtWithData<NewTrackToPlaylist>= {
      jwt: this.jwt,
      data: {
        track: {
          name: this.oldPlaylistWithTracks.items[this.index].track.name,
          author: this.oldPlaylistWithTracks.items[this.index].track.artists[0].name,
          uri: this.oldPlaylistWithTracks.items[this.index].track.uri,
        },
        playlistId: this.newPlaylist.id.toString()
      }
      
    }
    console.log(body);
    
    this.service.AddTrackToPlaylist(this.toStreamer, body)
      .subscribe({
        next: (res) => {
            this.index++;
            console.log(body.data.track.name + " added to " + body.data.playlistId);
            if(this.index <= this.oldPlaylistWithTracks.total!)
              this.addTrack();
          },
          error: (err) => {
            console.error(err);
          }
        });
  }
}
