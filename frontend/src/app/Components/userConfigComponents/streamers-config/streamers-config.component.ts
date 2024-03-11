import { Component } from '@angular/core';
import { StreamerService } from 'src/app/services/Streamer.Service';
import { jwt } from 'src/app/services/UserDto';
import { Alert } from 'src/app/services/Funcionalities';
import { streamers } from 'src/streamers';
@Component({
  selector: 'app-streamers-config',
  templateUrl: './streamers-config.component.html',
  styleUrls: ['./streamers-config.component.css']
})
export class StreamersConfigComponent {
  constructor( 
    private streamerService : StreamerService
  ){ }

  protected streamers = streamers.streamers;

  public Jwt : jwt = {
    value : "",
  }

  ngOnInit() {
    this.Jwt.value = sessionStorage.getItem("jwt") ?? "";

    if(this.Jwt.value == ""){
      window.location.href = "/login";
    }
  }

  alert : Alert = {
    isAlert : false,
    message : '',
    title : '',
    functionToCall: undefined,
  }

  logoffTogle( streamer : string ){
    this.alertShow("Are you shure","You will be logged out of Spotify", () => this.logoffSpotify(streamer));
  }

  alertShow (title : string, message : string, functionToCall? : Function) {
    this.alert.isAlert = true;
    this.alert.message = message;
    this.alert.title = title;
    this.alert.functionToCall = functionToCall;
  }
  setAlertFunction () {
    
  }
  closeAlert(){
    this.alert.isAlert = false;
    this.alert.message = "";
    this.alert.title = "";
    this.alert.functionToCall = undefined;
  }

  //! transform in class to others streamers use!!!
  logoffSpotify(streamer : string){
    var jwtValue = sessionStorage.getItem("jwt") ?? "";
    var jwtt: jwt ={
      value: jwtValue,
    };

    if(jwtt.value == "")
      window.location.href = "/login";
    this.streamerService.LogOff(streamer,jwtt).subscribe({
      next: data => {
        window.location.reload();
      },
      error: error => {
        console.log(error);
      }
    });
  }
}

