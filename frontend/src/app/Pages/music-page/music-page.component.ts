import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { itemsOfPlaylist } from 'src/app/services/SpotifyDto';

@Component({
  selector: 'app-music-page',
  templateUrl: './music-page.component.html',
  styleUrls: ['./music-page.component.css']
})
export class MusicPageComponent {
  private id : string = "";
  private streamer : string = "";
  // private streamerService: IStreamerService;

  constructor ( 
    private activatedRoute : ActivatedRoute,
  ){
    //! modular conforme o streamer 
    // this.streamerService = new SpotifyService()
  }

  ngOnInit(){
    this.getTrackData();
  }

  getTrackData() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.streamer = params['streamer'];
      console.log(this.id);
      console.log(this.streamer);
    });

    if (this.streamer == "spotify") {
      // this.streamerService.GetTrackContent({jwt: localStorage.getItem("jwt"), data: id}).subscribe((data: any) => {})
    }
  }
}
