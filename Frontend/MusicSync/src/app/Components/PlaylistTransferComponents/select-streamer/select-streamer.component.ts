import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TransferPlaylistObject } from 'src/app/services/SpotifyDto';

@Component({
  selector: 'app-select-streamer',
  templateUrl: './select-streamer.component.html',
  styleUrls: ['./select-streamer.component.css', '../main-card/main-card.component.css']
})
export class SelectStreamerComponent {
  @Output() nextClicked = new EventEmitter();
  streamerForm! : FormGroup;
  
  ngOnInit(){
    this.streamerForm = new FormGroup({
      from: new FormControl("",[Validators.required]),
      to:   new FormControl("",[Validators.required])
    })
  }

  get from(){
    return this.streamerForm.get('from');
  }
  get to(){
    return this.streamerForm.get('to');
  }

  next(){
    const obj : TransferPlaylistObject = {
      identifier: "streamer",
      data: {
        from: this.from?.value,
        to: this.to?.value
      }
    }
    if(this.streamerForm.invalid){
      return;
    }
    this.nextClicked.emit(obj);
  }
}
